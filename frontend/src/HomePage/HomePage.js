import React from "react";
import "./HomePage.css";
import heroShape from "../assets/hero-shape.png";
import healthDroplet from "../assets/HealthDroplet.png";

const HomePage = () => (
  <div className="homepage">
    {/* Navbar */}
    <nav className="navbar">
      <div className="logo">
        <img src={healthDroplet} alt="Health Droplet Logo" className="logo-img" />
      </div>
      <ul className="nav-links">
        <li>News</li>
        <li className="active">Home</li>
        <li>About Us</li>
        <li>Find Blood</li>
        <li>Register Now ▼</li>
      </ul>
      <button className="login-btn">Log In</button>
    </nav>

    {/* Hero Section */}
    <div className="hero-section">
      <img src={heroShape} alt="Hero Shape" className="hero-img" />
      <div className="hero-content">
        <h1>Save Life Donate Blood</h1>
        <p>
          Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
        </p>
        <button className="cta-btn">Get Blood Now</button>
      </div>
    </div>

    {/* Mission Section */}
    <section className="mission-section">
      <h2>Our Mission</h2>
      <p>
        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
      </p>
    </section>

    {/* Collaborators Section */}
    <section className="collaborators-section">
      <h2>Our Collaborators</h2>
      <div className="collaborators">
        <div className="collaborator">NCC</div>
        <div className="collaborator">NSS</div>
        <div className="collaborator">YMCA</div>
      </div>
      <div className="carousel-dots">
        <span className="dot active"></span>
        <span className="dot"></span>
        <span className="dot"></span>
      </div>
    </section>

    {/* How To Get Blood Section */}
    <section className="how-to-section">
      <h2>How to get Blood?</h2>
      <div className="how-to-steps">
        <div className="how-step">
          <div className="step-number">1</div>
          <div className="step-content">
            <div className="step-icon">
              <svg width="48" height="48" fill="none" stroke="#222" strokeWidth="2">
                <rect x="12" y="12" width="24" height="24" rx="4"/>
                <path d="M16 32l16-16M16 16h16v16"/>
              </svg>
            </div>
            <div className="step-desc">
              Lorem Ipsum is simply dummy text of the printing and typesetting industry.
            </div>
          </div>
        </div>
        <div className="how-step">
          <div className="step-number">2</div>
          <div className="step-content">
            <div className="step-icon">
              <svg width="48" height="48" fill="none" stroke="#222" strokeWidth="2">
                <rect x="12" y="12" width="24" height="24" rx="4"/>
                <path d="M16 32l16-16M16 16h16v16"/>
              </svg>
            </div>
            <div className="step-desc">
              Lorem Ipsum is simply dummy text of the printing and typesetting industry.
            </div>
          </div>
        </div>
        <div className="how-step">
          <div className="step-number">3</div>
          <div className="step-content">
            <div className="step-icon">
              <svg width="48" height="48" fill="none" stroke="#222" strokeWidth="2">
                <rect x="12" y="12" width="24" height="24" rx="4"/>
                <path d="M16 32l16-16M16 16h16v16"/>
              </svg>
            </div>
            <div className="step-desc">
              Lorem Ipsum is simply dummy text of the printing and typesetting industry.
            </div>
          </div>
        </div>
        <div className="heartbeat">
          <svg width="320" height="60" viewBox="0 0 320 60" fill="none">
            <path d="M0 30 Q40 10 60 30 Q80 50 100 30 Q120 10 140 30 Q160 50 180 30 Q200 10 220 30 Q240 50 260 30 Q280 10 320 30" stroke="#8B1B3A" strokeWidth="3" fill="none"/>
            <path d="M160 30 Q160 20 170 20 Q180 20 180 30 Q180 40 170 40 Q160 40 160 30 Z" fill="#8B1B3A"/>
          </svg>
        </div>
      </div>
    </section>

    {/* Footer */}
    <footer className="footer">
      <div className="footer-top">
        <div className="footer-logo">
          <span role="img" aria-label="drop" className="footer-drop">💧</span>
        </div>
        <div className="footer-cta">
          <span>Ready to get started?</span>
          <button className="donate-btn">Donate</button>
        </div>
      </div>
      <hr className="footer-divider" />
      <div className="footer-content">
        <div className="footer-newsletter">
          <div className="newsletter-title">Subscribe to our newsletter</div>
          <form className="newsletter-form">
            <input type="email" placeholder="Email address" />
            <button type="submit" className="newsletter-submit">{'>'}</button>
          </form>
        </div>
        <div className="footer-links">
          <div>
            <div className="footer-link-title">Services</div>
            <div className="footer-link">Email Marketing</div>
            <div className="footer-link">Campaigns</div>
            <div className="footer-link">Branding</div>
            <div className="footer-link">Offline</div>
          </div>
          <div>
            <div className="footer-link-title">About</div>
            <div className="footer-link">Our Story</div>
            <div className="footer-link">Benefits</div>
            <div className="footer-link">Team</div>
            <div className="footer-link">Careers</div>
          </div>
          <div>
            <div className="footer-link-title">Help</div>
            <div className="footer-link">FAQs</div>
            <div className="footer-link">Contact Us</div>
          </div>
        </div>
      </div>
      <div className="footer-bottom">
        <div>
          <span className="footer-policy">Terms & Conditions</span>
          <span className="footer-policy">Privacy Policy</span>
        </div>
        <div className="footer-socials">
          <span className="footer-social"><i className="fab fa-facebook-f" /></span>
          <span className="footer-social"><i className="fab fa-twitter" /></span>
          <span className="footer-social"><i className="fab fa-instagram" /></span>
        </div>
      </div>
    </footer>
  </div>
);

export default HomePage;

