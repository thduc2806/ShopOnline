export const UrlBackEnd = process.env.REACT_APP_BACKEND_URL;

export const URL = process.env.REACT_APP_URL ?? "http://localhost:3000";

export const USER_PROFILE_STORAGE_KEY = "user_profile_storage_key";
export const REQUEST_ACCESS_TOKEN_STORAGE_KEY = "request_access_token_storage_key";

export const CallBackEndpoints = {
    redirect_uri: `${URL}/authentication/login-callback`,
    post_logout_redirect_uri: `${URL}/authentication/logout-callback`,
};