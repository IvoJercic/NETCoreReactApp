import "@coreui/coreui/dist/css/coreui.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { FlightProvider } from "./context/FlightContext";
import NavBar from "./components/NavBar";
import HomePage from "./components/Pages/HomePage";
import FavoritesPage from "./components/Pages/FavoritesPage";
import FlightLoaderWrapper from "./components/FlightLoaderWrapper";
import { ROUTES } from "./constants/Routes";

function App() {
    return (
        <FlightProvider>
            <Router>
                <FlightLoaderWrapper>
                    <NavBar />
                    <Routes>
                        <Route path={ROUTES.HOME} element={<HomePage />} />
                        <Route path={ROUTES.FAVORITES} element={<FavoritesPage />} />
                    </Routes>
                </FlightLoaderWrapper>
            </Router>
        </FlightProvider>
    );
}

export default App;
