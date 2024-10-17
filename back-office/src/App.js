import { BrowserRouter, Route, Routes } from "react-router-dom"
import BaseLayout from "./components/BaseLayout";
import PrivateRoute from "./components/PrivateRoute";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route exact path="/login" element={<Login />} />
        <Route exact path="/forgot-password" element={<ForgotPassword />} />
        <Route element={<PrivateRoute />}>
          <Route element={<BaseLayout />}>
            <Route exact path="/" element={<Dashboard />} />
            <Route exact path="/change-password" element={<ChangePassword />} />

            <Route exact path="/pages" element={<Pages />} />
            <Route exact path="/pages/create" element={<PageEdit />} />
            <Route exact path="/pages/edit/:id" element={<PageEdit />} />

            <Route exact path="/settings" />
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
