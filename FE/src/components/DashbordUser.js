import 'bootstrap-icons/font/bootstrap-icons.css'
import { Link, Outlet, useNavigate } from 'react-router-dom'
import axios from 'axios'
import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchPdt, addPdt } from '../Actions/PdtActions';
import './table.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import AddPdt from './AddPdt';

	const DashbordUser = () => {
		const navigate = useNavigate()
        const token = useSelector((state) => state.auth.token);

        useEffect(() => {
          if (token) {
            localStorage.setItem('token', token); // Store token in localStorage
            console.log('Token stored in localStorage');
          }
        }, [token]);
        
      const tokenFromCookie = localStorage.getItem('token');
      console.log('tokenFromCookie',tokenFromCookie);
	{/*const navigate = useNavigate()
	axios.defaults.withCredentials = true;
	useEffect(()=>{
		axios.get('http://localhost:8081/dashboard')
		.then(res => {
			if(res.data.Status === "Success") {
				if(res.data.role === "admin") {
					navigate('/');
				} else {
					const id = res.data.id;
					navigate('/employeedetail/'+id)
				}
			} else {
				navigate('/start')
			}
		})
	}, [])

	const handleLogout = () => {
		axios.get('http://localhost:8081/logout')
		.then(res => {
			navigate('/start')
		}).catch(err => console.log(err));
	}*/}
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
		<div className="container-fluid">
			<div className="row flex-nowrap">
            <div className="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark position-fixed" style={{ top: 70, bottom: 0 }}>
					<div className="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
						<a href="/" className="d-flex align-items-center pb-3 mb-md-1 mt-md-3 me-md-auto text-white text-decoration-none">
							<span className="fs-5 fw-bolder d-none d-sm-inline">User Dashboard</span>
						</a>
						<ul className="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
							{/*<li>
								<Link to="/" data-bs-toggle="collapse" className="nav-link text-white px-0 align-middle">
									<i className="fs-4 bi-speedometer2"></i> <span className="ms-1 d-none d-sm-inline">Dashboard</span> </Link>
    </li>*/}
							<li>
								<Link to="/ManageStock" className="nav-link px-0 align-middle text-white">
									<i className="fs-4 bi-basket"></i> <span className="ms-1 d-none d-sm-inline">Manage stock</span> </Link>
							</li>
							<li>
								<Link to="profile" className="nav-link px-0 align-middle text-white">
									<i className="fs-4 bi-person"></i> <span className="ms-1 d-none d-sm-inline">Profile</span></Link>
							</li>
							<li >
                                <a href="#" className="nav-link px-0 align-middle text-white" onClick={handleInLogin}>
									<i className="fs-4 bi-power"></i> <span className="ms-1 d-none d-sm-inline">Logout</span></a>
							</li>
						</ul>
					</div>
				</div>
				<div className="col p-0 m-0">
					<div className='p-2 d-flex justify-content-center shadow'>
						<h4>Stock Management System</h4>						
					</div>
					<Outlet />
				</div>
			</div>
		</div>
	)
}

export default DashbordUser