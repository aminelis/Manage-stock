import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchPdt, addPdt } from '../Actions/PdtActions';
import { useNavigate } from 'react-router-dom';
import './table.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import AddPdt from './AddPdt';

function ProductList() {
  const products = useSelector(state => state.products.products);
  const dispatch = useDispatch();
  const [searchValue, setSearchValue] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const usersPerPage = 10;
  const token = useSelector((state) => state.auth.token);

  useEffect(() => {
    if (token) {
      localStorage.setItem('token', token); // Store token in localStorage
      console.log('Token stored in localStorage');
    }
  }, [token]);
  
const tokenFromCookie = localStorage.getItem('token');
console.log('tokenFromCookie',tokenFromCookie);

  useEffect(() => {
    dispatch(fetchPdt(tokenFromCookie)); // Passez le token comme argument de l'action fetchPdt
    
  }, [dispatch, tokenFromCookie]);


  const handleUpdateList = () => {
    dispatch(fetchPdt());
  };

  const handleCreatePdt = (newPdt) => {
    dispatch(addPdt(newPdt));
  };

  const filteredProducts = products.filter((product) =>
  String(product.id).toLowerCase().includes(searchValue.toLowerCase()) ||
  (product.codePdt?.toLowerCase() ?? '').includes(searchValue.toLowerCase()) ||
  (product.produit.toLowerCase() ?? '').includes(searchValue.toLowerCase()) ||
  (product.prix.toLowerCase() ?? '').includes(searchValue.toLowerCase())
  );

  const indexOfLastUser = currentPage * usersPerPage;
  const indexOfFirstUser = indexOfLastUser - usersPerPage;
  const currentPdt = filteredProducts.slice(indexOfFirstUser, indexOfLastUser);
  const totalUsers = filteredProducts.length;
  const totalPages = Math.ceil(totalUsers / usersPerPage);

  const navigate = useNavigate();
  const handleInLogin = async () => {
    try {
      const response = await fetch('https://localhost:7013/api/Auth/logout', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        }});
      if (response.ok) {
        localStorage.removeItem('token');
        navigate('/Home');
      } else {
        console.error('Échec de l\'authentification');
      }
    } catch (error) {
      console.error('Erreur lors de la connexion :', error);
    }
  };



  return (
    <div className="container">
      <div className="row">
        <div className='col'></div>
        <div className="col-auto">
          <button type="button" className="btn btn-primary btn-lg ml-auto" onClick={handleInLogin}>Se deconnecter</button>
        </div>
      </div>
      <h1 className='NH1'>Liste des produits</h1>
      
      <div className="row">
        <div className='col-auto'>
          <AddPdt onCreatePdt={handleCreatePdt} onUpdateList={handleUpdateList} />
        </div>
      </div>

      <table className="table table-user table-bordered" >
        <thead>
          <tr>
            <th style={{ backgroundColor: 'lightblue' }}>Id</th>
            <th style={{ backgroundColor: 'lightblue' }}>Code produit</th>
            <th style={{ backgroundColor: 'lightblue' }}>Produit</th>
            <th style={{ backgroundColor: 'lightblue' }}>Prix</th>
          </tr>
        </thead>
        <tbody>
          {currentPdt.map(product => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.codePdt}</td>
              <td>{product.produit}</td>
              <td>{product.prix}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <button
          onClick={() => setCurrentPage(currentPage - 1)}
          disabled={currentPage === 1}
          type="button" className="btn btn-light btn-lg"
        >
          Previous
        </button>
        <span>Page {currentPage} of {totalPages}</span>
        <button
          onClick={() => setCurrentPage(currentPage + 1)}
          disabled={indexOfLastUser >= totalUsers}
          type="button" className="btn btn-light btn-lg"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default ProductList;
