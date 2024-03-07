
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../App.css';
import { Link } from 'react-router-dom';
import {
  MDBBtn,
  MDBContainer,
  MDBInput,
  MDBCheckbox,
} from 'mdb-react-ui-kit';
import { useDispatch } from 'react-redux';
import { login } from '../Actions/PdtActions';

function EmployeeLoginn() {
  const dispatch = useDispatch();
  const navigate = useNavigate;
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false); // État local pour stocker la valeur de "Remember me"

  useEffect(() => {
    // Récupérer la valeur de "Remember me" du localStorage lors du chargement du composant
    const rememberMeValue = localStorage.getItem('rememberMe');
    if (rememberMeValue) {
      setRememberMe(JSON.parse(rememberMeValue));
    }
  }, []);

  const handleLogin = async () => {
    try {
      await dispatch(login(username, password)); // Appel de l'action login avec les données d'identification
      if (rememberMe) {
        localStorage.setItem('rememberMe', JSON.stringify(true));
      } else {
        localStorage.removeItem('rememberMe');
      }
      navigate('/productList'); // Redirection vers la page productList après la connexion réussie
    } catch (error) {
      console.error('Error logging in:', error);
    }
  };

  const handleRememberMeChange = () => {
    // Fonction pour mettre à jour l'état de "Remember me"
    setRememberMe(!rememberMe); // Inverser la valeur actuelle de "Remember me"
  };

  return (
    <MDBContainer className="p-3 my-5 d-flex flex-column w-50">
      <MDBInput
        wrapperClass="mb-4"
        label="Username"
        id="form1"
        type="text"
        onChange={(e) => setUsername(e.target.value)}
      />
      <MDBInput
        wrapperClass="mb-4"
        label="Password"
        id="form2"
        type="password"
        onChange={(e) => setPassword(e.target.value)}
      />
      <div className="d-flex justify-content-between mx-3 mb-4">
        <MDBCheckbox
          name="flexCheck"
          value=""
          id="flexCheckDefault"
          label="Remember me"
          checked={rememberMe} // L'état de la case à cocher est basé sur la valeur de "Remember me"
          onChange={handleRememberMeChange} // Appel de la fonction lors du changement d'état de la case à cocher
        />
        <a href="!#">Forgot password?</a>
      </div>
      <MDBBtn className="mb-4" onClick={handleLogin}>
        Sign in
      </MDBBtn>
      <div className="text-center">
        <p>
          Not a member? <Link to="/Registration">Register</Link>
        </p>
      </div>
    </MDBContainer>
  );
}

export default EmployeeLoginn;
