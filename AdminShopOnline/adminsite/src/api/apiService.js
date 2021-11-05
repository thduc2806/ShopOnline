import axios from 'axios';
let API_URL = "https://localhost:5001/api";
export function callApi(endpoint, method = 'GET', body) {
    return axios({
        method,
        url: `${API_URL}/${endpoint}`,
        data: body,
        // headers: {
        //     'Content-Type': 'multipart/form-data'
        // }
    }).catch(e => {
        console.log(e)
    })
}

export function GET_ALL_PRODUCTS(endpoint) {
    return callApi(endpoint, "GET");
}
export function GET_PRODUCT_ID(endpoint, id) {
    return callApi(endpoint + "/" + id, "GET");
}
export function POST_ADD_PRODUCT(endpoint, data) {
    return callApi(endpoint, "POST", data);
}
export function PUT_EDIT_PRODUCT(endpoint, data) {
    return callApi(endpoint, "PUT", data);
}
export function DELETE_PRODUCT_ID(endpoint) {
    return callApi(endpoint, "DELETE");
}
export function GET_ALL_CATEGORY(endpoint) {
    return callApi(endpoint, "GET");
}
export function GET_CATEGORY_ID(endpoint, id) {
    return callApi(endpoint + "/" + id, "GET");
}
export function DELETE_CATEGORY_ID(endpoint) {
    return callApi(endpoint, "DELETE");
}
export function POST_ADD_CATEGORY(endpoint, data) {
    return callApi(endpoint, "POST", data);
}
export function PUT_EDIT_CATEGORY(endpoint, data) {
    return callApi(endpoint, "PUT", data);
}
export function GET_ALL_USERS(endpoint) {
    return callApi(endpoint, "GET");
}
export function GET_USERS_ID(endpoint, id) {
    return callApi(endpoint + "/" + id, "GET");
}
export function DELETE_USERS_ID(endpoint) {
    return callApi(endpoint, "DELETE");
}
export function PUT_EDIT_USERS(endpoint) {
    return callApi(endpoint, "PUT");
}
export function POST_ADD_USERS(endpoint, data) {
    return callApi(endpoint, "POST", data);
}
