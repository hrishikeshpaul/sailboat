import React from 'react';

import { ChakraProvider } from '@chakra-ui/react';
import ReactDOM from 'react-dom';

import { theme } from 'theme/ChakraTheme';
import { App } from './App';
import { reportWebVitals } from './reportWebVitals';

import 'aos/dist/aos.css';
import 'intro.js/introjs.css';
import './index.scss';

ReactDOM.render(
    <React.StrictMode>
        <ChakraProvider theme={theme}>
            <App />
        </ChakraProvider>
    </React.StrictMode>,
    document.getElementById('root'),
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
