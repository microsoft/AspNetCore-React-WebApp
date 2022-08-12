import * as msal from '@azure/msal-browser';
import { MsalProvider } from '@azure/msal-react';
import App from 'app/App';
import React from 'react';

const msalConfig = {
    auth: {
        clientId: process.env.REACT_APP_CLIENT_ID,
        authority: process.env.REACT_APP_TENANT_ID
    },
    cache: {
        cacheLocation: 'localStorage' as 'localStorage'
    }
} as msal.Configuration;

const msalInstance = new msal.PublicClientApplication(msalConfig);

const MsalComponent: React.FC = () => {
    return (
        <MsalProvider instance={msalInstance}>
            <App />
        </MsalProvider>
    );
};

export default MsalComponent;
