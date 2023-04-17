import axios from 'axios';
let API_URL = "https://localhost:5001/api";
export function callApi(endpoint, method = 'GET', body) {
    console.log(localStorage.getItem("request_access_token_storage_key"));
    return axios({
        method,
        url: `${API_URL}/${endpoint}`,
        data: body,
        headers: {
            Authorization: `Bearer ${localStorage.getItem("request_access_token_storage_key")}`
        }
    }).catch(e => {
        console.log(e)
    })
}

export function POST_ADD_PRODUCT(endpoint, data) {
    return callApi(endpoint, "POST", data);
}