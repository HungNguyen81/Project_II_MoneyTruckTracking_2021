import React from 'react';
import { useState } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";


const FormBody = (props) => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    let history = useHistory();

    const handleLoginSubmit = (e) => {
        e.preventDefault();        
        // console.log(username, password);
        axios.post('https://localhost:44343/api/accounts/login', 
                { 
                    username: username, 
                    password: password
                })
        .then(response => {                          
            history.push(
                {
                    pathname:'/home',
                    state: { data: response.data }
                });
            history.go(0);
            // console.log(response);
            // this.props.history.push({
            //     pathname: '/home',
            //     state: { detail: response.data }
            //   })
        })
        .catch(e => {
            console.log(e);
            setError("Wrong Username or Password!");
        });

    }

    return (
        <div className="container">
            <h1>GIÁM SÁT HÀNH TRÌNH</h1>
            {(error !== "") ? (<div className="error">{error}</div>) : ""}
            <form onSubmit={handleLoginSubmit}>
                <input type="text"
                    value={username}
                    onChange={e => setUsername(e.target.value)}
                    placeholder="Tên đăng nhập" required></input>
                <input
                    type="password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    placeholder="Mật khẩu" required></input>
                <p id="forget-pw">Quên mật khẩu ? Bấm vào <a href='#'>đây</a></p>
                <input type="submit"
                    id="signin-btn"
                    value="ĐĂNG NHẬP"></input>
            </form>
        </div>

    );
};

export default FormBody;