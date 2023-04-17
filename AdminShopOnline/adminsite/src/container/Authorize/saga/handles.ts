import { PayloadAction } from "@reduxjs/toolkit";
import { call, put } from "redux-saga/effects";

import { Status } from "../../../Constants/status";
import IError from "../../..//interface/IError";
import ILoginModel from "../../..//interface/ILoginModel";
import ISubmitAction from "../../..//interface/ISubmitAction";

import { setAccount, setStatus } from "../reducer";
import { loginRequest} from './request';

export function* handleLogin(action: PayloadAction<ILoginModel>) {
    const loginModel = action.payload;
    
    try {
        const {data} = yield call(loginRequest, loginModel);
        yield put(setAccount(data));

    } catch (error: any) {
        const errorModel = error.response.data as IError;

        yield put(setStatus({
            status: Status.Failed,
            error: errorModel,
        }));
    }
}
