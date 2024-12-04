import { NavLink } from 'react-router-dom';
import { ROUTES } from '../constants/Routes';

export default function NavBar() {
    return (
        <nav className="navbar">
            <NavLink to={ROUTES.HOME} className={({ isActive }) => (isActive ? "active" : "")}>
                Search
            </NavLink>
            <NavLink
                to={ROUTES.FAVORITES}
                className={({ isActive }) => (isActive ? "active" : "")}
            >
                Favorites
            </NavLink>
        </nav>
    );
}