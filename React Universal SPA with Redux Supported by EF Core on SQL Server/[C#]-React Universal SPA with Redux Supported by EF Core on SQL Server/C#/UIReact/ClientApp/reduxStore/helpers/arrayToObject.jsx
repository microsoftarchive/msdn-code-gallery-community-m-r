export const mapArrToObj = (srcArray, keyField) => {
    return srcArray.reduce((obj, cur) => {
        return { ...obj, [cur[keyField]]: cur}
    }, {});
}

export const mapArrToObjNoId = (srcArray, keyField) => {
    return srcArray.reduce((obj, cur) => {
        const curNoId = { ...cur };
        delete curNoId[keyField];

        const arrNoId = !obj[cur[keyField]]
            ? []
            : [...obj[cur[keyField]]];

        return { ...obj, [cur[keyField]]: [...arrNoId, curNoId] };
    }, {});
};

const mapStyleApiToRedux = (style) => {
    const obj = { ...style };
    delete obj.averageRatings;
    delete obj.reviewCount;
    delete obj.soldOut;

    return obj;
}

const mapStyleApiToState = (style) => {
    const obj = {
        styleId: style.styleId,
        averageRatings: style.averageRatings,
        reviewCount: style.reviewCount,
        soldOut: style.soldOut
    };

    return obj;
}

export function mapStylesToRedux(styles) {
    let styleObj = {};
    let stateObj = {};

    for (let i = 0; i < styles.length; i++) {
        const stylePart = mapStyleApiToRedux(styles[i]);
        const statePart = mapStyleApiToState(styles[i]);

        styleObj = { ...styleObj, [stylePart.styleId]: stylePart };
        stateObj = { ...stateObj, [statePart.styleId]: statePart };
    }

    return { styles: styleObj, states: stateObj };
}

