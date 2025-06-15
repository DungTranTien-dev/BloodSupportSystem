import React from 'react';
import { FaFacebook, FaTwitter, FaInstagram, FaLinkedin } from "react-icons/fa";

const Footer = () => {
  return (
    <footer className="bg-slate-900 text-slate-300 py-16 px-4">
      <div className="container mx-auto grid md:grid-cols-4 gap-12">
        <div>
          <h3 className="text-lg font-semibold text-white mb-4">LifeStream</h3>
          <p className="text-sm">Connecting donors, saving lives. Your contribution matters.</p>
          <div className="flex space-x-4 mt-6 text-xl">
            <a href="#" className="hover:text-red-500 transition-colors"><FaFacebook /></a>
            <a href="#" className="hover:text-red-500 transition-colors"><FaTwitter /></a>
            <a href="#" className="hover:text-red-500 transition-colors"><FaInstagram /></a>
            <a href="#" className="hover:text-red-500 transition-colors"><FaLinkedin /></a>
          </div>
        </div>
        <div>
          <h3 className="text-lg font-semibold text-white mb-4">Quick Links</h3>
          <ul className="space-y-2 text-sm text-slate-400">
            <li><a href="/about-us" className="hover:text-red-500 hover:underline">About Us</a></li>
            <li><a href="/donate" className="hover:text-red-500 hover:underline">Donate Blood</a></li>
            <li><a href="/find-blood" className="hover:text-red-500 hover:underline">Find Blood</a></li>
            <li><a href="/news" className="hover:text-red-500 hover:underline">News & Events</a></li>
          </ul>
        </div>
        <div>
          <h3 className="text-lg font-semibold text-white mb-4">Contact Us</h3>
          <ul className="space-y-2 text-sm">
            <li>Email: info@lifestream.org</li>
            <li>Phone: +1 (555) 123-4567</li>
            <li>Address: 123 Health St, Medcity</li>
          </ul>
        </div>
        <div>
          <h3 className="text-lg font-semibold text-white mb-4">Newsletter</h3>
          <p className="text-sm mb-4">Stay updated with our latest news and campaigns.</p>
          <div className="flex overflow-hidden rounded-md shadow-sm border border-slate-300">
            <input type="email" placeholder="Your email" className="w-full px-4 py-2 text-slate-800 focus:outline-none" />
            <button className="bg-gradient-to-r from-red-500 to-pink-500 text-white px-4 font-semibold">Subscribe</button>
          </div>
        </div>
      </div>
      <div className="border-t border-slate-700 mt-12 pt-8 text-center text-sm">
        <p>© {new Date().getFullYear()} LifeStream. All rights reserved. Made with ❤️ for a better world.</p>
      </div>
    </footer>
  );
};

export default Footer;