import { BrowserRouter as Router, Routes, Link, Route } from 'react-router-dom';
import { useNavigate } from 'react-router-dom'
import { useState } from 'react';
import ProductList from './components/productList';
import Home from './components/Home';
import { Provider } from 'react-redux';
import store from './Store/store';
import Registration from './components/Registration';
import AddPdt from './components/AddPdt';
import EmployeeLogin from './components/employeeLogin';
import DashbordUser from './components/DashbordUser';
import Footer from './Footer';
import ManageStock from './components/ManageStock';

function App() {
  

  return (
    <Provider store={store}>
    <div className="App background-image">
      <nav>
        <ul>
            <Link to="/" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}>
              
            </Link>
              <Link to="/productList" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}>
                
              </Link>
              <Link to="/Home" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}>
                
              </Link>
              <Link to="/Registration" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}/>
              <Link to="/AddPdt" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}/>
              <Link to="/employeeLogin" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}/>
              <Link to="/DashbordUser" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}/>
              <Link to="/ManageStock" style={{ color: 'blue', textDecoration: 'underline', marginBottom: '10px' }}/>
              
        </ul>
      </nav>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/productList" element={<ProductList />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/Registration" element={<Registration />} />
        <Route path="/AddPdt" element={<AddPdt />} />
        <Route path="/employeeLogin" element={<EmployeeLogin />} />
        <Route path="/DashbordUser" element={<DashbordUser />} />
        <Route path="/ManageStock" element={<ManageStock />} />
      </Routes>
      <Footer />
    </div>
  </Provider>
  );
}

export default App;

