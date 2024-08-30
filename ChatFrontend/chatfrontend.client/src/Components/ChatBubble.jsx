import PropTypes from "prop-types";
import "./Chat.css";

const ChatBubble = ({ text = "", sender = "sent", time = "" }) => {
    console.log("Sender:", sender);
    return (
        <div className={`message ${sender}`}>
            {text} <span className="time">{time}</span>
        </div>
    );
};

ChatBubble.propTypes = {
    text: PropTypes.string.isRequired,
    sender: PropTypes.string.isRequired,
    time: PropTypes.string.isRequired,
};

export default ChatBubble;
