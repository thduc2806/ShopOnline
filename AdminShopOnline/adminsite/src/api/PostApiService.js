import axios from 'axios';
let API_URL = "https://localhost:5001/api";
export function callApi(endpoint, method = 'GET', body) {
    return axios({
        method,
        url: `${API_URL}/${endpoint}`,
        data: body,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    }).catch(e => {
        console.log(e)
    })
}

export function POST_ADD_PRODUCT(endpoint, data) {
    return callApi(endpoint, "POST", data);
}