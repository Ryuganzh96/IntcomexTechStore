import axios from "axios";
import { verificarToken } from "./authService";
import { API_CONFIG } from "./config"; // Importar la configuración

// Este archivo contiene la logica para interactuar con el backend



// Funcion para enviar una solicitud al endpoint de articulos
export const fetchArticulos = async (categoria, cantidad, pagina) => {
    try {
        const token = await verificarToken(); // se verifica si el token es válido o si se necesita uno nuevo

        // Prepara los datos del cuerpo de la solicitud
        const requestBody = {
            categoria,
            cantidad,
            pagina,
        };

        // Configura la solicitud con el token en la cabecera
        const response = await axios.post(
            `${API_CONFIG.BASE_URL}/Articulos/listado`,
            requestBody,
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );

        return response.data; // Devuelve los datos de la respuesta
    } catch (error) {
        console.error("Error al obtener los artículos:", error);
        throw error;
    }
};

// Funcion para obtener articulos del backend
export async function fetchArticles(category, quantity, page) {
    try {
        const token = await verificarToken();
        const response = await axios.post(`${API_CONFIG.BASE_URL}/articulos/listado`, {
            categoria: category,
            cantidad: quantity,
            pagina: page
        }, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching articles:', error);
        return [];
    }
}


