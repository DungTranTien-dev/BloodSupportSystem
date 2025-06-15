/**
 * Dashboard Constants
 * 
 * Contains all static data and configuration for the admin dashboard
 * Centralized location for menu items, statistics, and other constants
 * 
 * @author Your Name
 * @version 1.0.0
 */

import { 
  FaTachometerAlt, 
  FaUsers, 
  FaUser, 
  FaSearch, 
  FaEdit, 
  FaEnvelope,
  FaCog,
  FaChartBar,
  FaBell
} from 'react-icons/fa';

/**
 * Navigation menu items for the admin sidebar
 * Each item contains icon, label, path, and description
 */
export const MENU_ITEMS = [
  { 
    icon: FaTachometerAlt, 
    label: 'Dashboard', 
    path: '/admin/dashboard',
    description: 'Main dashboard overview with key metrics',
    category: 'main'
  },
  { 
    icon: FaUsers, 
    label: 'Blood Group', 
    path: '/admin/blood-group',
    description: 'Manage blood group types and categories',
    category: 'main'
  },
  { 
    icon: FaUser, 
    label: 'Donor List', 
    path: '/admin/donor-list',
    description: 'View and manage registered donors',
    category: 'main'
  },
  { 
    icon: FaSearch, 
    label: 'Manage Contactus Query', 
    path: '/admin/contactus',
    description: 'Handle contact form queries and support requests',
    category: 'main'
  },
  { 
    icon: FaEdit, 
    label: 'Manage Pages', 
    path: '/admin/pages',
    description: 'Content management system for website pages',
    category: 'content'
  },
  { 
    icon: FaEdit, 
    label: 'Update Contact Info', 
    path: '/admin/contact-info',
    description: 'Update organization contact details',
    category: 'settings'
  },
  { 
    icon: FaEnvelope, 
    label: 'Request Received By Donar', 
    path: '/admin/requests',
    description: 'Manage blood donation requests from donors',
    category: 'main'
  },
  { 
    icon: FaChartBar, 
    label: 'Analytics', 
    path: '/admin/analytics',
    description: 'View detailed analytics and reports',
    category: 'reports'
  },
  { 
    icon: FaBell, 
    label: 'Notifications', 
    path: '/admin/notifications',
    description: 'Manage system notifications and alerts',
    category: 'settings'
  },
  { 
    icon: FaCog, 
    label: 'Settings', 
    path: '/admin/settings',
    description: 'System configuration and preferences',
    category: 'settings'
  }
];

/**
 * Default statistics data for dashboard cards
 * In real application, this would come from API
 */
export const DEFAULT_STATISTICS = [
  {
    id: 'blood-groups',
    number: '6',
    title: 'LISTED BLOOD GROUPS',
    bgColor: 'bg-gradient-to-br from-red-500 to-red-600',
    fullDetailText: 'FULL DETAIL',
    description: 'Total number of blood group types in system',
    icon: FaUsers,
    trend: '+2.5%',
    trendDirection: 'up'
  },
  {
    id: 'registered-groups',
    number: '9',
    title: 'REGISTERED BLOOD GROUP',
    bgColor: 'bg-gradient-to-br from-pink-500 to-pink-600',
    fullDetailText: 'FULL DETAIL',
    description: 'Active registered blood groups',
    icon: FaUser,
    trend: '+5.2%',
    trendDirection: 'up'
  },
  {
    id: 'total-queries',
    number: '0',
    title: 'TOTAL QUERIES',
    bgColor: 'bg-gradient-to-br from-red-400 to-pink-500',
    fullDetailText: 'FULL DETAIL',
    description: 'Contact form queries received',
    icon: FaSearch,
    trend: '0%',
    trendDirection: 'neutral'
  },
  {
    id: 'blood-requests',
    number: '5',
    title: 'TOTAL BLOOD REQUEST RECEIVED',
    bgColor: 'bg-gradient-to-br from-pink-400 to-red-500',
    fullDetailText: 'FULL DETAIL',
    description: 'Blood donation requests from donors',
    icon: FaEnvelope,
    trend: '+12.3%',
    trendDirection: 'up'
  }
];

/**
 * Color scheme constants for consistent theming
 */
export const COLORS = {
  primary: {
    50: '#eff6ff',
    100: '#dbeafe',
    500: '#3b82f6',
    600: '#2563eb',
    700: '#1d4ed8',
    800: '#1e40af',
    900: '#1e3a8a'
  },
  success: {
    50: '#f0fdf4',
    100: '#dcfce7',
    500: '#22c55e',
    600: '#16a34a',
    700: '#15803d'
  },
  warning: {
    50: '#fffbeb',
    100: '#fef3c7',
    500: '#f59e0b',
    600: '#d97706'
  },
  danger: {
    50: '#fef2f2',
    100: '#fee2e2',
    500: '#ef4444',
    600: '#dc2626'
  },
  gray: {
    50: '#f9fafb',
    100: '#f3f4f6',
    200: '#e5e7eb',
    300: '#d1d5db',
    400: '#9ca3af',
    500: '#6b7280',
    600: '#4b5563',
    700: '#374151',
    800: '#1f2937',
    900: '#111827'
  }
};

/**
 * Application metadata and configuration
 */
export const APP_CONFIG = {
  name: 'BloodBank & Donor Management System',
  version: '1.0.0',
  description: 'Comprehensive blood bank management solution',
  author: 'Your Development Team',
  lastUpdated: '2024-12-11'
};

/**
 * Responsive breakpoints for layout
 */
export const BREAKPOINTS = {
  sm: '640px',
  md: '768px',
  lg: '1024px',
  xl: '1280px',
  '2xl': '1536px'
};

/**
 * Animation durations in milliseconds
 */
export const ANIMATIONS = {
  fast: 150,
  normal: 300,
  slow: 500
};
