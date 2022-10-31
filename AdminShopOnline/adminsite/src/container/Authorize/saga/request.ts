import { AxiosResponse } from "axios";

import RequestService from '../../../service/request';
import EndPoints from '../../../Constants/endpoint';
import ILoginModel from "../../../interface/ILoginModel";
import IAccount from "../../../interface/IAccount";

export function loginRequest(login: ILoginModel): Promise<AxiosResponse<IAccount>> {
    return RequestService.axios
    .post(EndPoints.authentication, login)
    .then(response => {
        if (response.data) {
          localStorage.setItem('token', response.data.token);
          localStorage.setItem('user', response.data.user);
          
        }

        return response.data;
        
      });
    // .catch(
    //   function (error) {
    //     console.log('Show error notification!')
    //     localStorage.setItem('message', error.response.data.message);
    //     console.log(error.response.data.message)
    //   }
    // );    
}
