import axios from "axios";
import { verificarToken } from "./authService";
import { API_CONFIG } from "./config"; // Importar la configuraci�n

// Este archivo contiene la l�gica para interactuar con el backend



// Funci�n para enviar una solicitud al endpoint de art�culos
export const fetchArticulos = async (categoria, cantidad, pagina) => {
    try {
        const token = await verificarToken(); // Verifica si el token es v�lido o si se necesita uno nuevo

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
        console.error("Error al obtener los art�culos:", error);
        throw error;
    }
};

// Funci�n para obtener articulosdel backend
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


