import React from 'react';
import { render } from '@testing-library/react';
import App from 'app/App';

describe('<App />', () => {
    it('renders without crashing', () => {
        render(<App />);
    });
});
