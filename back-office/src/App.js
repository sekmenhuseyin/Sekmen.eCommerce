import { BrowserRouter, Route, Routes } from "react-router-dom"
import BaseLayout from "./layouts/BaseLayout";
import PrivateRoute from "./components/PrivateRoute";
import Dashboard from "./pages/Dashboard";
import Page403 from "./pages/403";
import Page404 from "./pages/404";
import Users from "./pages/users/Users";
import Login from "./pages/auth/Login";
import ForgotPassword from "./pages/auth/ForgotPassword";
import Coupons from "./pages/coupons/Coupons";
import Products from "./pages/products/Products";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route exact path="/login" element={<Login />} />
        <Route exact path="/forgot-password" element={<ForgotPassword />} />
        <Route element={<PrivateRoute />}>
          <Route element={<BaseLayout />}>
            <Route exact path="/" element={<Dashboard />} />
            <Route exact path="/coupons" element={<Coupons />} />
            <Route exact path="/products" element={<Products />} />
            <Route exact path="/users" element={<Users />} />

            <Route exact path="/forbidden" element={<Page403 />} />
            <Route path="*" element={<Page404 />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
