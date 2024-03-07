import React, { useState } from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function AddPdt({ onCreatePdt, onUpdateList }) {
  const [CodePdt, setCodePdt] = useState('');
  const [Produit, setProduit] = useState('');
  const [Prix, setPrix] = useState('');

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleCreatePdt = async () => {
    try {
      console.log({ CodePdt, Produit, Prix })
      const formData = new FormData();
formData.append('CodePdt', CodePdt);
formData.append('Produit', Produit);
formData.append('Prix', Prix);

const response = await axios.post('https://localhost:7013/api/Product/AddProduct', formData);
      console.log(response)
      if (response.status === 200) {
        const newPdt = {
          CodePdt: CodePdt,
          Produit: Produit,
          Prix: Prix
        };
    
        onCreatePdt(newPdt);
        onUpdateList(); 
  
        toast.success('Ajout effectué avec succès');
        // Réinitialiser le formulaire après une création réussie
        setCodePdt('');
        setProduit('');
        setPrix('');
        handleClose(); // Fermer la modal
      } else {
        console.error('Erreur lors de la création du produit.');
        toast.error('Erreur lors de la création du produit.');
      }
    } catch (error) {
      console.error('Erreurs lors de la création du produit:', error);
      if (error.response && error.response.data && error.response.data.errors) {
        const validationErrors = error.response.data.errors;
        Object.keys(validationErrors).forEach((field) => {
          console.error(`Erreur de validation pour le champ ${field}: ${validationErrors[field]}`);
        });
      }
      toast.error('Erreur lors de la création du produit.');
    }
  };

  return (
    <>
      <button type="button" className="btn btn-success btn-lg" style={{ marginBottom: '20px' }} onClick={handleShow}>
        <span className="icon">
          <FontAwesomeIcon icon={faPlus} />
        </span>{" "}
        Ajouter
      </button>

      <Modal show={show} onHide={handleClose} backdrop="static">
        <Modal.Header closeButton>
          <Modal.Title>Ajouter un produit</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Group controlId="formCodePdt">
            <div className="row mt-3">
              <div className="col-md-4">
                <Form.Label>Code du produit :</Form.Label>
              </div>
              <div className="col-md-8">
                <Form.Control
                  type="text"
                  placeholder="Code du produit"
                  value={CodePdt}
                  onChange={(e) => setCodePdt(e.target.value)}
                />
              </div>
            </div>
          </Form.Group>
          <Form.Group controlId="formProduit">
            <div className="row mt-3">
              <div className="col-md-4">
                <Form.Label>Nom du produit :</Form.Label>
              </div>
              <div className="col-md-8">
                <Form.Control
                  type="text"
                  placeholder="Nom du produit"
                  value={Produit}
                  onChange={(e) => setProduit(e.target.value)}
                />
              </div>
            </div>
          </Form.Group>
          <Form.Group controlId="formPrix">
            <div className="row mt-3">
              <div className="col-md-4">
                <Form.Label>Prix du produit :</Form.Label>
              </div>
              <div className="col-md-8">
                <Form.Control
                  type="text"
                  placeholder="Prix du produit"
                  value={Prix}
                  onChange={(e) => setPrix(e.target.value)}
                />
              </div>
            </div>
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Annuler
          </Button>
          <Button variant="primary" onClick={handleCreatePdt}>
            Enregistrer
          </Button>
        </Modal.Footer>
      </Modal>

      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </>
  );
}

export default AddPdt;
