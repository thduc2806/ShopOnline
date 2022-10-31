import { createSlice, PayloadAction } from "@reduxjs/toolkit";

import { SetStatusType } from "../../Constants/status";
import IAccount from "../../interface/IAccount";
import IError from "../../interface/IError";
import ILoginModel from "../../interface/ILoginModel";
import ISubmitAction from "../../interface/ISubmitAction";
import request from "../../service/request";
// import { getUsersRequest } from "src/components/services/request";
import { getLocalStorage, removeLocalStorage, setLocalStorage } from "../../utils/localStorage";

type AuthState = {
    loading: boolean;
    isAuth: boolean,
    changePassword: boolean;
    account?: IAccount;
    status?: number;
    error?: IError;
}

const token = getLocalStorage('token');

const initialState: AuthState = {
    isAuth: !!token,
    loading: false,
    changePassword: true,
};

const AuthSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setAccount: (state: AuthState, action: PayloadAction<IAccount>): AuthState => {
            const account = action.payload;
            if (account?.token) {
                setLocalStorage('token', account.token);
                request.setAuthentication(account.token);
                // getUsersRequest().then;
                account.changePassword
            }

            return {
                ...state,
                // status: Status.Success,
                changePassword: true,
                account,
                isAuth: true,
                loading: false,
            };
        },
        setStatus: (state: AuthState, action: PayloadAction<SetStatusType>) => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            }
        },
        me: (state) => {
            if (token) {
                request.setAuthentication(token);
            }
        },
        login: (state: AuthState, action: PayloadAction<ILoginModel>) => ({
            ...state,
            isAuth: true,
            loading: true,
            changePassword: true,
        }),
        logout: (state: AuthState) => {

            removeLocalStorage('token');
            request.setAuthentication('')

            return {
                ...state,
                isAuth: false,
                account: undefined,
                status: undefined,
            };
        },
        cleanUp: (state) => ({
            ...state,
            loading: false,
            status: undefined,
            error: undefined,
        }),
    }
});

export const {
    setAccount, login, setStatus, me, logout, cleanUp,
} = AuthSlice.actions;

export default AuthSlice.reducer;