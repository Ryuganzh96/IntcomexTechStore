import axios from "axios";
import { API_CONFIG } from "./config"; // Asegúrate de que se importe la configuración

// Este archivo contiene la lógica para la autenticación y gestión del token

let token = null;
let tokenExpirationTime = null;

// Función para autenticar y obtener un token nuevo
export const autenticar = async () => {
    try {
        // Llave codificada en Base64 desde la configuración
        const encodedKey = API_CONFIG.SECRET_KEY;

        // Realiza la solicitud al endpoint de autenticación
        const response = await axios.post(
            `${API_CONFIG.BASE_URL}/Auth/authenticate`,
            { EncodedKey: encodedKey }
        );

        // Guarda el token y su tiempo de expiración
        token = response.data.token;
        tokenExpirationTime = Date.now() + 3600 * 1000; // Por ejemplo, 1 hora

        return token;
    } catch (error) {
        console.error("Error en la autenticación:", error);
        throw error;
    }
};

// Función para verificar si el token es válido o si debe obtenerse uno nuevo
export const verificarToken = async () => {
    if (!token || Date.now() >= tokenExpirationTime) {
        return await autenticar(); // Si no hay token o ha expirado, se obtiene uno nuevo
    }
    return token; // Si el token es válido, se devuelve
};
