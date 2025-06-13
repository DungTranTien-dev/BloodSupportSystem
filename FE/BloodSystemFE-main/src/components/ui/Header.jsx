import React from 'react';
import { useNavigate } from 'react-router-dom';
import { FaHeartbeat } from 'react-icons/fa'; // Sử dụng icon khác cho logo

const NavLink = ({ children, onClick }) => (
  <li
    className="font-medium text-slate-700 hover:text-red-600 transition-colors duration-300 cursor-pointer"
    onClick={onClick}
  >
    {children}
  </li>
);

function Header() {
  const navigate = useNavigate();

  return (
    <header className="sticky top-0 z-50 w-full bg-white/80 backdrop-blur-md shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-20">
          {/* Logo Section */}
          <div 
            className="flex items-center cursor-pointer"
            onClick={() => navigate("/")}
          >
            <FaHeartbeat className="h-8 w-8 text-red-600" />
            <span className="ml-3 text-2xl font-bold text-slate-800">
              Life<span className="text-pink-600">Stream</span>
            </span>
          </div>

          {/* Desktop Navigation */}
          <nav className="hidden md:flex">
            <ul className="flex items-center space-x-8 bold">
              <NavLink onClick={() => navigate("/")}>Home</NavLink>
              <NavLink onClick={() => navigate("/blog")}>Blog</NavLink>
              <NavLink onClick={() => navigate("/about-us")}>About Us</NavLink>
              <NavLink onClick={() => navigate("/bloodtype")}>Blood Group</NavLink>
            </ul>
          </nav>

          {/* === PHẦN THAY ĐỔI: THÊM NÚT REGISTER === */}
          {/* Auth Buttons */}
          <div className="flex items-center space-x-3">
            <button
              className="px-6 py-2.5 rounded-full font-semibold text-white bg-gradient-to-r from-red-500 to-pink-600 hover:shadow-lg hover:shadow-pink-500/40 transform hover:-translate-y-0.5 transition-all duration-300"
              onClick={() => navigate("/login")}
            >
              Login
            </button>
            <button
              className="px-3.5 py-2.5 rounded-full font-semibold text-white bg-gradient-to-r from-red-500 to-pink-600 hover:shadow-lg hover:shadow-pink-500/40 transform hover:-translate-y-0.5 transition-all duration-300"
              onClick={() => navigate("/register")}
            >
              Register
            </button>
          </div>
          {/* === KẾT THÚC PHẦN THAY ĐỔI === */}
          
          {/* Mobile menu button can be added here if needed */}
        </div>
      </div>
    </header>
  );
}

export  default Header;