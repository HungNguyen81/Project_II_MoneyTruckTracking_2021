import React from 'react';
import {useHistory} from 'react-router-dom'

const Home = () => {    
    const history = useHistory();
    return(
        <>        
        <h1 className="homeTitle">HI <i>{history.location.state.data.name}</i>! THIS IS HOME PAGE </h1>        
        </>
    )
}

export default Home;