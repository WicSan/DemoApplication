import React, { FC } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { actionCreators, selectCount} from '../store/Counter';

const Counter: FC<{}> = () => {
    const dispatch = useDispatch();
    const count = useSelector(selectCount);

    return (
        <div id="increment">
            <h1>Counter</h1>

            <p>This is a simple example of a React component.</p>

            <p aria-live="polite">Current count: <strong>{count}</strong></p>

            <button type="button"
                className="btn btn-primary btn-lg"
                onClick={() => { dispatch(actionCreators.increment()) }}>
                Increment
            </button>
        </div>
    );
}


export default Counter;