import { useState } from "react";
import PropTypes from "prop-types";
import { fetchArticles } from "../services/apiService";
import { API_CONFIG } from "../services/config";
import "./Chat.css";

const MessageInput = ({ onSend }) => {
    const [input, setInput] = useState("");
    const [canSendMessage, setCanSendMessage] = useState(true);

    const validateMessage = (message) => {
        const parts = message.split(',');
        if (parts.length !== 3) return false;
        const [, quantity, page] = parts; // Omite la variable 'category' si no se usa
        return isNaN(quantity) === false && isNaN(page) === false;
    };

    const formatJson = (json) => {
        return JSON.stringify(json, null, 2)
            .replace(/,/g, ',\n')
            .replace(/{/g, '{\n')
            .replace(/}/g, '\n}');
    };

    const handleChange = (e) => {
        if (e.target.value.length <= API_CONFIG.MAX_CHARACTERS) {
            setInput(e.target.value);
        }
    };

    const handleSendMessage = async () => {
        const message = input.trim();
        if (!canSendMessage) {
            onSend(`Debes esperar ${API_CONFIG.MESSAGE_DELAY / 1000} segundos para enviar otro mensaje.`, "received");
            return;
        }

        // Enviar mensaje del usuario
        onSend(message, "sent");

        if (validateMessage(message)) {
            const [category, quantity, page] = message.split(',');
            const articles = await fetchArticles(category.trim(), Number(quantity), Number(page));
            const formattedArticles = formatJson(articles);
            onSend(`articulosencontrados:\n${formattedArticles}`, "received");
        } else {
            onSend("Formato incorrecto. Debe ser: categoria,cantidad,pagina.", "received");
        }

        setInput(""); // Limpiar el campo de texto después de enviar el mensaje
        setCanSendMessage(false); // Desactivar envío de mensajes

        // Reactivar envío de mensajes después de MESSAGE_DELAY
        setTimeout(() => {
            setCanSendMessage(true);
        }, API_CONFIG.MESSAGE_DELAY);
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            handleSendMessage();
        }
    };

    return (
        <div className="chat-footer">
            <input
                type="text"
                value={input}
                onChange={handleChange}
                onKeyDown={handleKeyDown}
                placeholder="Write a message"
            />
            <button onClick={handleSendMessage}>Enviar</button>
        </div>
    );
};

MessageInput.propTypes = {
    onSend: PropTypes.func.isRequired,
};

export default MessageInput;
