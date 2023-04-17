import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { library } from '@fortawesome/fontawesome-svg-core'
import store from "./redux/store";
import "bootstrap/dist/css/bootstrap.min.css";
import "nprogress/nprogress.css";
import "react-notifications/lib/notifications.css";
import "react-datepicker/dist/react-datepicker.css";
import "./styles/App.scss";
import { Provider } from "react-redux";
import {Routes} from "./routes"


function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes />
      <BrowserRouter />
  );
}


// const rootElement = document.getElementById('root');
// ReactDOM.render(
//   <BrowserRouter>
//     <App />
//   </BrowserRouter>,
//   rootElement);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals

