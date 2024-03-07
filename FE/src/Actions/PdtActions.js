// reunionActions.js
import { ADD_PDT, FETCH_Pdts,SET_TOKEN, DELETE_REUNIONS, UPDATE_REUNIONS } from '../constants/constants';
import axios from 'axios';


export const Login = (username, password, role) => {
  return async (dispatch) => {
    try {
      const response = await axios.post('https://localhost:7013/api/Auth/login', {
        username,
        password,
        role
      }, {
        timeout: 100000 // Délai d'attente de 10 secondes
      });
      
      if (response.status === 200) {
        const token = response.data.token;
        dispatch(setToken(token));
        console.log('New state after setting token:', token);
      } else {
        // Si la réponse n'est pas 200, quelque chose s'est mal passé
        throw new Error('Invalid response status: ' + response.status);
      }
    } catch (error) {
      console.error('Error logging in:', error);
      throw error; // Lancer explicitement l'erreur pour la propager
    }
  };
};


export const setToken = (token) => {
  return {
    type: SET_TOKEN,
    payload: token
  };
};

export const addPdt = (product) => {
  return {
    type: ADD_PDT,
    payload: product,
  };
};

// Action creator pour récupérer les réunions depuis l'API
export const fetchPdt = (token) => {
  return async (dispatch) => {
    try {
      // Effectuez votre appel à l'API ici
      const response = await axios.get('https://localhost:7013/api/Product/GetProductsById', {
        headers: {
          Authorization: `Bearer ${token}` // Incluez le token JWT dans les en-têtes de la requête
        }
      });
      dispatch({
        type: FETCH_Pdts,
        payload: response.data
      });
    } catch (error) {
      console.error('Error fetching Produits:', error);
    }
  };
};

