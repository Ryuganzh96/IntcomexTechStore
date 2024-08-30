import axios from "axios";
import { API_CONFIG } from "./config"; //se importa la configuración

// Este archivo contiene la logica para la autenticacion y gestion del token

let token = null;
let tokenExpirationTime = null;

// Funcion para autenticar y obtener un token nuevo
export const autenticar = async () => {
    try {
        // Llave codificada en Base64 desde la configuración
        const encodedKey = API_CONFIG.SECRET_KEY;

        // Realiza la solicitud al endpoint de autenticación
        const response = await axios.post(
            `${API_CONFIG.BASE_URL}/Auth/authenticate`,
            { EncodedKey: encodedKey }
        );

        // se guarda el token y su tiempo de expiración
        token = response.data.token;
        tokenExpirationTime = Date.now() + 3600 * 1000; // Por ejemplo, 1 hora
        tokenExpirationTime = Date.now() + API_CONFIG.TOKEN_EXPIRATION_TIME; 

        return token;
    } catch (error) {
        console.error("Error en la autenticación:", error);
        throw error;
    }
};

// Funcion para verificar si el token es valido o si debe obtenerse uno nuevo
export const verificarToken = async () => {
    if (!token || Date.now() >= tokenExpirationTime) {
        return await autenticar(); // Si no hay token o ha expirado, se obtiene uno nuevo
    }
    return token; // Si el token es valido, se devuelve
};
