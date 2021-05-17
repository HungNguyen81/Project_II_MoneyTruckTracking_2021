import React from 'react';
import FormBody from './form-body';
import FormFooter from './form-footer';
import ContactFooter from './contact-footer';

const Login = () => {
    return (
        <>
            <div className="section1" id="body">
                <FormBody />
            </div>
            <div className="section2 flex-container">
                <FormFooter />
            </div>
            <div className="section3 flex-container">
                <ContactFooter />
            </div>
        </>
    )
}

export default Login;