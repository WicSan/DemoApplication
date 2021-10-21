import React from 'react';
import '@testing-library/jest-dom'
import Counter from './Counter';
import { render } from '../test-utils';

describe('Counter', () => {
  it('should render "Current count: 0"', () => {
    const { getByText } = render(<Counter/>);
    expect(getByText('Current count:')).toBeInTheDocument();
  });
});