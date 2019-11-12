import { addStyles } from '../actions/handleAllStyles';
import { mapStylesToRedux } from './arrayToObject'

export function dispatchStyles(dispatch, actionStyles, actionStates, styles) {
    const styleIds = styles.map(style => style.styleId);
    dispatch(actionStyles(styleIds));

    dispatchStylesOnly(dispatch, actionStates, styles);
};

export function dispatchStylesOnly(dispatch, actionStates, styles) {
    const styleRedux = mapStylesToRedux(styles);
    dispatch(addStyles(styleRedux.styles));
    dispatch(actionStates(styleRedux.states));
}


