import React, { useState } from 'react';
import '../App.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { Login } from '../Actions/PdtActions';

const EmployeeLogin = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [checkMe, setCheckMe] = useState('user'); // 'user' comme valeur initiale
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const [Token, setToken] = useState('');
    const dispatch = useDispatch();

    const handleLogin = async (e) => {
        e.preventDefault(); // Empêcher le rechargement de la page lors de la soumission du formulaire
        try {
            await dispatch(Login(username, password,checkMe)); // Attendre que la fonction dispatch se termine
            console.log('bonsoir');
            navigate('/DashbordUser'); // Redirection vers la page productList après la connexion réussie
        } catch (error) {
            console.error('Error logging in:', error);
            setError('Error logging in'); // Définir l'erreur dans l'état local pour l'afficher dans l'interface utilisateur
        }
    };

    return (
      <div className='d-flex justify-content-center align-items-center vh-100'>
          <div className='p-3 rounded w-25 border loginPage'>
              
              <h2 style={{ color: 'white' }}>Login</h2>
              <form onSubmit={handleLogin}>
                  <div className='mb-3'>
                      <label htmlFor='UserName'>
                          <strong style={{ color: 'white' }}>UserName</strong>
                      </label>
                      <input
                          type='text'
                          placeholder='Enter UserName'
                          name='UserName'
                          onChange={(e) => setUsername(e.target.value)}
                          className='form-control rounded-0'
                          autoComplete='off'
                      />
                  </div>
                  <div className='mb-3'>
                      <label htmlFor='password'>
                          <strong style={{ color: 'white' }}>Password</strong>
                      </label>
                      <input
                          type='password'
                          placeholder='Enter Password'
                          name='password'
                          onChange={(e) => setPassword(e.target.value)}
                          className='form-control rounded-0'
                      />
                  </div>
                  <div className='mb-3 d-flex align-items-center'> {/* Utilisez d-flex pour aligner les éléments horizontalement */}
                      <input
                          type='checkbox'
                          id='isAdmin'
                          checked={checkMe === 'admin'}
                          style={{ width: '20px', height: '20px', marginRight: '10px' }}
                          onChange={() => setCheckMe(checkMe === 'admin' ? 'user' : 'admin')}
                      />
                      <label htmlFor='isAdmin' style={{ color: 'white' }}>
                          <strong>Admin ?</strong>
                      </label>
                  </div>
                  <button type='submit' className='btn btn-success w-100 rounded-0'>
                      Log in
                  </button>
                  <div className='text-danger' style={{marginTop: '10px'}}>{error && error}</div>
              </form>
          </div>
      </div>
  );
  
};

export default EmployeeLogin;


