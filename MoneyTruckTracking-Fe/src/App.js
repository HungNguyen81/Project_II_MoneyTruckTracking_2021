import React from 'react';
import './App.css';

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from 'react-router-dom';

import Home from './Components/Home/home';
import Login from './Components/Login/login';
import history from './history';

function App() {
  return (    
    <Router history={history}>
      <Switch>
        <Route exact path="/">
          <Redirect to="/login" />
        </Route>
        <Route path="/login" exact={true}>
          <Login />
        </Route>
        <Route path="/home" exact={true}>          
          <div className="section1" id="body">
            <Home />
          </div>
        </Route>
      </Switch>
    </Router>    
  );
}

export default App;
