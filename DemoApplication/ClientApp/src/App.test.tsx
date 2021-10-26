import * as React from 'react';
import { Provider } from 'react-redux';
import { MemoryRouter } from 'react-router-dom';
import App from './App';
import './custom.css'
import { render } from '@testing-library/react';

it('renders without crashing', () => {
    const storeFake = (state: any) => ({
        default: () => {},
        subscribe: () => {},
        dispatch: () => {},
        getState: () => ({ ...state })
    });
    const store = storeFake({}) as any;

    render(<Provider store={store}>
            <MemoryRouter>
              <App/>
            </MemoryRouter>
        </Provider>);
});
