import { ADD_PDT, FETCH_Pdts, DELETE_REUNIONS, UPDATE_REUNIONS } from '../constants/constants';

const initialState = {
  products: [],
};

const productReducers = (state = initialState, action) => {
  switch (action.type) {
    case ADD_PDT:
      return {
        ...state,
        products: [...state.products, action.payload],
      };
    case FETCH_Pdts:
      return {
        ...state,
        products: action.payload,
      };
      case UPDATE_REUNIONS:
      return {
        ...state,
        reunions: [...state.reunions, action.payload],
      };
    default:
      return state;
  }
};

export default productReducers;