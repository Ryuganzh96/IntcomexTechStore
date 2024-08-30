import { useState } from "react";
import Header from "./Header";
import ChatBubble from "./ChatBubble";
import MessageInput from "./MessageInput";
import "./Chat.css";

const Chat = () => {
    const [messages, setMessages] = useState([
        {
            text: "Hola,bienvenido al chat de Intcomex para consultar articulos, recuerda que debes enviar una categoria o subcagetoria, el numero total de articulos a mostrar y el numero total de paginas, separados cada uno por una coma",
            sender: "received",
            time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
        }
    ]);

    // Agrega un nuevo mensaje a la lista
    const addMessage = (text, sender) => {
        const newMessage = {
            text,
            sender,
            time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
        };
        setMessages((prevMessages) => [...prevMessages, newMessage]);
    };

    return (
        <div className="chat-container">
            <Header />
            <div className="message-container">
                {messages.map((msg, index) => (
                    <ChatBubble key={index} text={msg.text} sender={msg.sender} time={msg.time} />
                ))}
            </div>
            <MessageInput onSend={(message) => addMessage(message, "sent")} />
        </div>
    );
};

export default Chat;
