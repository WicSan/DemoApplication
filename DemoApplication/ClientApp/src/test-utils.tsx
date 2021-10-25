// test-utils.jsx
import React, { ReactElement } from 'react'
import { configureStore, EnhancedStore } from '@reduxjs/toolkit'
import { render, RenderResult} from '@testing-library/react'
import { Provider } from 'react-redux'
import { reducers } from './store'

const customRender = (
  ui: ReactElement,
  {
    preloadedState,
    store = configureStore({ reducer: { ...reducers }, preloadedState }),
    ...renderOptions
  } : { preloadedState?: any, store?: EnhancedStore } = {}
) : RenderResult => {
  const wrapper: React.FC = ({ children }) => {
    return <Provider store={store}>{children}</Provider>
  }
  return render(ui, { wrapper, ...renderOptions })
}

export * from '@testing-library/react'
export { customRender as render }