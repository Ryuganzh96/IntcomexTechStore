import axios from "axios";
import { API_CONFIG } from "./config"; // Aseg�rate de que se importe la configuraci�n

// Este archivo contiene la l�gica para la autenticaci�n y gesti�n del token

let token = null;
let tokenExpirationTime = null;

// Funci�n para autenticar y obtener un token nuevo
export const autenticar = async () => {
    try {
        // Llave codificada en Base64 desde la configuraci�n
        const encodedKey = API_CONFIG.SECRET_KEY;

        // Realiza la solicitud al endpoint de autenticaci�n
        const response = await axios.post(
            `${API_CONFIG.BASE_URL}/Auth/authenticate`,
            { EncodedKey: encodedKey }
        );

        // Guarda el token y su tiempo de expiraci�n
        token = response.data.token;
        tokenExpirationTime = Date.now() + 3600 * 1000; // Por ejemplo, 1 hora

        return token;
    } catch (error) {
        console.error("Error en la autenticaci�n:", error);
        throw error;
    }
};

// Funci�n para verificar si el token es v�lido o si debe obtenerse uno nuevo
export const verificarToken = async () => {
    if (!token || Date.now() >= tokenExpirationTime) {
        return await autenticar(); // Si no hay token o ha expirado, se obtiene uno nuevo
    }
    return token; // Si el token es v�lido, se devuelve
};
