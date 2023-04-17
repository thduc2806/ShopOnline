import { takeLatest } from 'redux-saga/effects';

import {login, me } from '../reducer';
import { handleLogin} from '../saga/handles';

export default function* authorizeSagas() {
    yield takeLatest(login.type, handleLogin)

}
