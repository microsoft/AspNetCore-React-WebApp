import { runWithAdal } from 'react-adal';

import { authContext } from './adalConfig';
import ReactDOM from 'react-dom';
import React from 'react';
import App from 'app/App';

// set to false after configuring environment variables and adalConfig.ts
const DO_NOT_LOGIN = true;

runWithAdal(
    authContext,
    () => {
        ReactDOM.render(<App />, document.getElementById('root'));
    },
    DO_NOT_LOGIN
);
