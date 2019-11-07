const getOptions = (data) => {
    return {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        credentials: 'include', 
        cache: 'default',
        body: JSON.stringify(data)
    }
}

export const getOptionsNoCredentials = (data) => {
    const options = getOptions(data);

    options.credentials = 'omit';

    return options;
}

export default getOptions;