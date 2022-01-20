const IsNullOrEmpty = (val) => {
    if (!val || val == undefined || val == "")
        return true;

    return false;
}

const IsNullOrWhiteSpace = (val) => {
    if (!val || val == undefined || !val.trim())
        return true;

    return false;
}

export { IsNullOrEmpty, IsNullOrWhiteSpace };