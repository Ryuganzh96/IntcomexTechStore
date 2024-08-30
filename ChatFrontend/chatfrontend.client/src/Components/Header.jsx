import botImage from "../assets/botImage.png";
import { FaPhone, FaVideo, FaEllipsisV } from "react-icons/fa";
import "./Chat.css";

function Header() {
    return (
        <div className="chat-header">
            <img src={botImage} alt="Chat Bot" className="chat-bot-image" />
            <div className="chat-title-container">
                <div className="chat-title">Chat de articulos</div>
                <div className="chat-status">En Linea</div>
            </div>
            <div className="chat-icons">
                <FaPhone className="icon" />
                <FaVideo className="icon" />
                <FaEllipsisV className="icon" />
            </div>
        </div>
    );
}

export default Header;
