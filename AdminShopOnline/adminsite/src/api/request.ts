import axios, { AxiosResponse } from 'axios';
import qs from 'qs';

import RequestService from '../service/request';

import EndPoints from '../Constants/endpoint';

export function Login(Form) {
    return RequestService.axios.post(EndPoints.authentication, Form);
}