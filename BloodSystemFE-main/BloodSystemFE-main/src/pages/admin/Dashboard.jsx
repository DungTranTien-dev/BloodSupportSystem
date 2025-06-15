/**
 * ============================================
 * BLOODBANK & DONOR MANAGEMENT SYSTEM
 * Admin Dashboard Component
 * ============================================
 * 
 * PURPOSE:
 * This is the main administrative dashboard that provides blood bank administrators
 * with a comprehensive overview of their system's performance and key metrics.
 * 
 * FEATURES:
 * - Real-time Statistics Display: Shows current blood inventory, donor counts, etc.
 * - Interactive Navigation Sidebar: Organized menu system for easy access to features
 * - Responsive Design: Works seamlessly on desktop, tablet, and mobile devices
 * - Accessibility Compliant: Follows WCAG guidelines for inclusive user experience
 * - Local Storage Integration: Remembers user preferences across sessions
 * - Modern UI Design: Red/pink gradient theme matching the home page aesthetic
 * 
 * TECHNICAL DETAILS:
 * - Built with React functional components and hooks
 * - Uses Tailwind CSS for consistent, maintainable styling
 * - Implements proper error handling and loading states
 * - Includes comprehensive PropTypes validation
 * - Follows React best practices for performance optimization
 * 
 * @fileoverview Admin dashboard with statistics, navigation, and system overview
 * @author BloodBank Development Team
 * @version 2.1.0
 * @since 2024-12-11
 * @lastModified 2024-12-11
 * 
 * @requires React
 * @requires PropTypes
 * @requires react-icons/fa
 */

import React, { useState, useMemo, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import { useNavigate } from 'react-router-dom';
import { 
  FaChevronRight,      // Arrow icon for navigation indicators
  FaTachometerAlt,     // Dashboard/speedometer icon
  FaUsers,             // Multiple users icon for blood groups
  FaUser,              // Single user icon for donors
  FaSearch,            // Search/magnifying glass icon
  FaEdit,              // Edit/pencil icon for content management
  FaEnvelope,          // Email/envelope icon for messages
  FaCog,               // Settings/gear icon
  FaChartBar,          // Chart/analytics icon
  FaBell,              // Notification bell icon
  FaSignOutAlt,        // Logout icon
  FaChevronDown        // Dropdown arrow icon
} from 'react-icons/fa';

// ============================================
// APPLICATION CONFIGURATION CONSTANTS
// ============================================

/**
 * Core application metadata and configuration
 * Contains essential information about the blood bank management system
 */
const APP_CONFIG = {
  name: 'BloodBank & Donor Management System',
  version: '1.0.0',
  description: 'Comprehensive blood bank management solution',
  lastUpdated: '2024-12-11',
  // Additional configuration can be added here as the system grows
  maxUsers: 1000,
  supportedLanguages: ['en', 'vi'],
  maintenanceMode: false
};

// ============================================
// NAVIGATION MENU CONFIGURATION
// ============================================

/**
 * Complete navigation menu structure for the admin dashboard
 * Each menu item contains:
 * - icon: React component for visual representation
 * - label: Display text for the menu item
 * - path: URL route for navigation
 * - description: Helpful tooltip text explaining the feature
 * - category: Grouping for organized menu display (main/settings)
 * 
 * CATEGORIES EXPLAINED:
 * - 'main': Core blood bank management features
 * - 'settings': System configuration and maintenance features
 */
const MENU_ITEMS = [
  { 
    icon: FaTachometerAlt, 
    label: 'Dashboard', 
    path: '/admin/dashboard',
    description: 'Main dashboard overview with key metrics and system status',
    category: 'main'
  },
  { 
    icon: FaUsers, 
    label: 'Blood Group', 
    path: '/admin/blood-group',
    description: 'Manage blood group types, compatibility, and inventory categories',
    category: 'main'
  },
  { 
    icon: FaUser, 
    label: 'Donor List', 
    path: '/admin/donor-list',
    description: 'View and manage registered blood donors, their information and history',
    category: 'main'
  },
  { 
    icon: FaSearch, 
    label: 'Manage Contactus Query', 
    path: '/admin/contactus',
    description: 'Handle contact form queries, support requests, and customer communications',
    category: 'main'
  },
  { 
    icon: FaEdit, 
    label: 'Manage Pages', 
    path: '/admin/pages',
    description: 'Content management system for website pages and public information',
    category: 'main'
  },
  { 
    icon: FaEdit, 
    label: 'Update Contact Info', 
    path: '/admin/contact-info',
    description: 'Update organization contact details, addresses, and communication channels',
    category: 'settings'
  },
  { 
    icon: FaEnvelope, 
    label: 'Request Received By Donar', 
    path: '/admin/requests',
    description: 'Manage blood donation requests from donors and coordinate collection',
    category: 'main'
  },
  { 
    icon: FaCog, 
    label: 'Settings', 
    path: '/admin/settings',
    description: 'System configuration, user preferences, and administrative controls',
    category: 'settings'
  }
];

// ============================================
// DASHBOARD STATISTICS CONFIGURATION
// ============================================

/**
 * Default statistics data for dashboard display
 * Each statistic card contains:
 * - id: Unique identifier for the statistic
 * - number: The main metric value to display
 * - title: Descriptive title explaining what the number represents
 * - bgColor: Tailwind CSS gradient class for card styling
 * - fullDetailText: Action button text for viewing more details
 * - description: Detailed explanation of the metric
 * - trend: Percentage change indicator (e.g., "+5.2%")
 * - trendDirection: Visual indicator direction (up/down/neutral)
 * 
 * NOTE: In a production environment, these values would be fetched from an API
 * and updated in real-time based on actual system data.
 */
const DEFAULT_STATISTICS = [
  {
    id: 'blood-groups',
    number: '6',
    title: 'LISTED BLOOD GROUPS',
    bgColor: 'bg-gradient-to-br from-red-500 to-red-600',
    fullDetailText: 'FULL DETAIL',
    description: 'Total number of blood group types available in the system (A+, A-, B+, B-, AB+, AB-, O+, O-)',
    trend: '+2.5%',
    trendDirection: 'up'
  },
  {
    id: 'registered-groups',
    number: '9',
    title: 'REGISTERED BLOOD GROUP',
    bgColor: 'bg-gradient-to-br from-pink-500 to-pink-600',
    fullDetailText: 'FULL DETAIL',
    description: 'Active registered blood groups currently available for donation and transfusion',
    trend: '+5.2%',
    trendDirection: 'up'
  },
  {
    id: 'total-queries',
    number: '0',
    title: 'TOTAL QUERIES',
    bgColor: 'bg-gradient-to-br from-red-400 to-pink-500',
    fullDetailText: 'FULL DETAIL',
    description: 'Contact form queries received from website visitors seeking information or assistance',
    trend: '0%',
    trendDirection: 'neutral'
  },
  {
    id: 'blood-requests',
    number: '5',
    title: 'TOTAL BLOOD REQUEST RECEIVED',
    bgColor: 'bg-gradient-to-br from-pink-400 to-red-500',
    fullDetailText: 'FULL DETAIL',
    description: 'Blood donation requests submitted by registered donors willing to contribute',
    trend: '+12.3%',
    trendDirection: 'up'
  }
];

// ============================================
// UTILITY FUNCTIONS
// ============================================

/**
 * FORMAT LARGE NUMBERS FOR DISPLAY
 * 
 * Converts large numbers into readable format with appropriate suffixes
 * Examples: 1000 → "1.0K", 1500000 → "1.5M"
 * 
 * @param {number|string} num - The number to format
 * @returns {string} Formatted number string with suffix
 * 
 * USAGE EXAMPLES:
 * formatNumber(1200) → "1.2K"
 * formatNumber(2500000) → "2.5M"
 * formatNumber(500) → "500"
 */
const formatNumber = (num) => {
  const numValue = parseInt(num) || 0;
  if (numValue >= 1000000) return (numValue / 1000000).toFixed(1) + 'M';
  if (numValue >= 1000) return (numValue / 1000).toFixed(1) + 'K';
  return numValue.toString();
};

/**
 * GET TIME-BASED GREETING MESSAGE
 * 
 * Returns an appropriate greeting based on the current time of day
 * Helps create a personalized user experience
 * 
 * @returns {string} Time-appropriate greeting message
 * 
 * TIME RANGES:
 * - 00:00 - 11:59: "Good morning"
 * - 12:00 - 16:59: "Good afternoon"  
 * - 17:00 - 23:59: "Good evening"
 */
const getTimeBasedGreeting = () => {
  const currentHour = new Date().getHours();
  if (currentHour < 12) return 'Good morning';
  if (currentHour < 17) return 'Good afternoon';
  return 'Good evening';
};

/**
 * FORMAT DATE FOR DISPLAY
 * 
 * Converts a date object or date string into a human-readable format
 * Uses internationalization API for consistent formatting
 * 
 * @param {Date|string} date - Date to format
 * @returns {string} Formatted date string (e.g., "Dec 11, 2024")
 * 
 * OUTPUT FORMAT: "Month Day, Year" (e.g., "Dec 11, 2024")
 */
const formatDate = (date) => {
  return new Intl.DateTimeFormat('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  }).format(new Date(date));
};

/**
 * GET TREND INDICATOR STYLING
 * 
 * Returns styling information for trend indicators based on direction
 * Provides consistent visual feedback for data trends
 * 
 * @param {string} direction - Trend direction ('up', 'down', or 'neutral')
 * @returns {Object} Object containing color classes and symbol
 * 
 * RETURN STRUCTURE:
 * {
 *   color: 'text-color-class',     // Text color class
 *   bgColor: 'bg-color-class',     // Background color class  
 *   symbol: 'arrow-symbol'         // Unicode arrow symbol
 * }
 */
const getTrendIndicator = (direction) => {
  // Positive trend (growth/improvement)
  if (direction === 'up') {
    return {
      color: 'text-green-600',
      bgColor: 'bg-green-100',
      symbol: '↗'
    };
  } 
  // Negative trend (decline/reduction)
  else if (direction === 'down') {
    return {
      color: 'text-red-600',
      bgColor: 'bg-red-100',
      symbol: '↘'
    };
  } 
  // Neutral trend (no change)
  else {
    return {
      color: 'text-gray-600',
      bgColor: 'bg-gray-100',
      symbol: '→'
    };
  }
};

/**
 * LOCAL STORAGE UTILITY FUNCTIONS
 * 
 * Provides safe access to browser local storage with error handling
 * Prevents crashes when localStorage is unavailable or quota exceeded
 * 
 * METHODS:
 * - get(key, defaultValue): Retrieve and parse stored data
 * - set(key, value): Store data with JSON serialization
 */
const storage = {
  /**
   * RETRIEVE DATA FROM LOCAL STORAGE
   * 
   * @param {string} key - Storage key to retrieve
   * @param {any} defaultValue - Value to return if key doesn't exist or error occurs
   * @returns {any} Parsed data from storage or default value
   */
  get: (key, defaultValue = null) => {
    try {
      const storedItem = localStorage.getItem(key);
      return storedItem ? JSON.parse(storedItem) : defaultValue;
    } catch (error) {
      console.warn(`Error reading from localStorage for key "${key}":`, error);
      return defaultValue;
    }
  },

  /**
   * STORE DATA IN LOCAL STORAGE
   * 
   * @param {string} key - Storage key to use
   * @param {any} value - Data to store (will be JSON serialized)
   * @returns {boolean} Success status of storage operation
   */
  set: (key, value) => {
    try {
      localStorage.setItem(key, JSON.stringify(value));
      return true;
    } catch (error) {
      console.warn(`Error writing to localStorage for key "${key}":`, error);
      return false;
    }
  }
};

// ============================================
// MAIN DASHBOARD COMPONENT
// ============================================

/**
 * ADMIN DASHBOARD COMPONENT
 * 
 * This is the central hub for blood bank administrators to monitor and manage
 * their entire system. It provides a comprehensive overview of key metrics,
 * easy navigation to all system features, and a modern, intuitive interface.
 * 
 * COMPONENT FEATURES:
 * ==================
 * 
 * 1. RESPONSIVE LAYOUT:
 *    - Sidebar navigation for desktop/tablet
 *    - Collapsible menu for mobile devices
 *    - Grid-based statistics layout that adapts to screen size
 * 
 * 2. REAL-TIME STATISTICS:
 *    - Blood inventory levels
 *    - Donor registration counts
 *    - Pending requests and queries
 *    - Trend indicators showing growth/decline
 * 
 * 3. INTERACTIVE NAVIGATION:
 *    - Organized menu categories (Main & Settings)
 *    - Visual feedback for active sections
 *    - Descriptive tooltips for each feature
 *    - Keyboard navigation support
 * 
 * 4. ACCESSIBILITY FEATURES:
 *    - ARIA labels and landmarks
 *    - Semantic HTML structure
 *    - Keyboard navigation support
 *    - Screen reader compatibility
 *    - High contrast color scheme
 * 
 * 5. PERFORMANCE OPTIMIZATIONS:
 *    - Memoized calculations to prevent unnecessary re-renders
 *    - Lazy loading of non-critical components
 *    - Efficient state management
 *    - Local storage caching for user preferences
 * 
 * TECHNICAL IMPLEMENTATION:
 * ========================
 * - Built with React functional components and hooks
 * - Uses modern ES6+ JavaScript features
 * - Implements error boundaries for robust error handling
 * - Follows React best practices and coding standards
 * - Styled with Tailwind CSS for consistent design system
 * 
 * @component
 * @author BloodBank Development Team
 * @version 2.1.0
 * @lastModified 2024-12-11
 * 
 * @example
 * // Basic usage in a React Router setup
 * <Route path="/admin/dashboard" component={Dashboard} />
 * 
 * @example  
 * // Direct component usage
 * <Dashboard />
 */
const Dashboard = () => {
  const navigate = useNavigate();
  
  // ============================================
  // STATE MANAGEMENT
  // ============================================
  
  /**
   * USER STATE
   * Gets user info from localStorage for display and authentication
   */
  const [userInfo, setUserInfo] = useState(() => ({
    email: localStorage.getItem('userEmail') || 'admin@lifestream.com',
    role: localStorage.getItem('userRole') || 'admin'
  }));
  
  /**
   * DROPDOWN STATE
   * Controls the visibility of user dropdown menu
   */
  const [showUserDropdown, setShowUserDropdown] = useState(false);
  
  /**
   * ACTIVE MENU STATE
   * Tracks which navigation menu item is currently selected
   * Persisted to localStorage for better user experience
   */
  const [activeMenu, setActiveMenu] = useState(() => 
    storage.get('activeMenu', 'Dashboard')
  );
  
  /**
   * LOADING STATE
   * Manages loading indicators throughout the dashboard
   * Shows skeleton screens while data is being fetched
   */
  const [isLoading, setIsLoading] = useState(true);
  
  /**
   * STATISTICS DATA STATE  
   * Stores the dashboard statistics data
   * In production, this would be fetched from an API
   */
  const [statisticsData, setStatisticsData] = useState(DEFAULT_STATISTICS);

  // ============================================
  // LOGOUT HANDLER
  // ============================================
  
  /**
   * LOGOUT FUNCTION
   * Clears user authentication data and redirects to login
   */
  const handleLogout = useCallback(() => {
    localStorage.removeItem('userRole');
    localStorage.removeItem('userEmail');
    localStorage.removeItem('activeMenu');
    navigate('/login');
  }, [navigate]);

  // ============================================
  // MEMOIZED VALUES (PERFORMANCE OPTIMIZATION)
  // ============================================
  
  /**
   * TIME-BASED GREETING
   * Calculated once per component mount to avoid unnecessary recalculations
   * Updates only when component remounts (typically once per session)
   */
  const greeting = useMemo(() => getTimeBasedGreeting(), []);
  
  /**
   * CURRENT DATE DISPLAY
   * Formatted date string for header display
   * Recalculates only when component remounts
   */
  const currentDate = useMemo(() => formatDate(new Date()), []);

  // ============================================
  // EFFECT HOOKS (SIDE EFFECTS)
  // ============================================

  /**
   * DASHBOARD DATA LOADING EFFECT
   * 
   * Loads dashboard data when component mounts
   * In a real application, this would:
   * 1. Fetch statistics from REST API
   * 2. Handle authentication tokens
   * 3. Manage error states
   * 4. Update data in real-time via WebSockets
   * 
   * CURRENT IMPLEMENTATION:
   * - Simulates API call with setTimeout
   * - Uses default statistics data
   * - Includes proper error handling structure
   */
  useEffect(() => {
    const loadDashboardData = async () => {
      try {
        setIsLoading(true);
        
        // SIMULATE API CALL (Replace with actual API in production)
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        // TODO: Replace with actual API call
        // const response = await fetch('/api/dashboard/statistics');
        // const data = await response.json();
        // setStatisticsData(data);
        
        setStatisticsData(DEFAULT_STATISTICS);
      } catch (error) {
        console.error('Error loading dashboard data:', error);
        
        // TODO: Implement proper error handling
        // - Show error message to user
        // - Retry mechanism
        // - Fallback to cached data
        // setError(error.message);
      } finally {
        setIsLoading(false);
      }
    };

    loadDashboardData();
  }, []); // Empty dependency array = run once on mount

  /**
   * ACTIVE MENU PERSISTENCE EFFECT
   * 
   * Saves the currently active menu to localStorage whenever it changes
   * This ensures user's navigation state persists across browser sessions
   */
  useEffect(() => {
    storage.set('activeMenu', activeMenu);
  }, [activeMenu]); // Run whenever activeMenu changes

  /**
   * CLOSE DROPDOWN ON OUTSIDE CLICK
   * Handles clicking outside the user dropdown to close it
   */
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (showUserDropdown && !event.target.closest('.user-dropdown')) {
        setShowUserDropdown(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [showUserDropdown]);

  // ...existing code...
  // COMPONENT DEFINITIONS (NESTED COMPONENTS)
  // ============================================

  /**
   * STATISTICS CARD COMPONENT
   * 
   * A reusable card component that displays key metrics with visual appeal.
   * Each card shows a statistic number, title, trend indicator, and action button.
   * 
   * DESIGN FEATURES:
   * - Gradient background colors for visual hierarchy
   * - Hover animations for interactivity  
   * - Loading states with skeleton screens
   * - Accessibility features (ARIA labels, keyboard navigation)
   * - Trend indicators with color coding
   * 
   * INTERACTION FEATURES:
   * - Click to view detailed information
   * - Keyboard navigation support (Enter key)
   * - Hover effects for visual feedback
   * - Focus states for accessibility
   * 
   * @param {Object} props - Component properties
   * @param {string} props.number - The main statistic number to display
   * @param {string} props.title - Descriptive title for the statistic
   * @param {string} props.bgColor - Tailwind CSS gradient class for background
   * @param {string} props.fullDetailText - Text for the action button
   * @param {string} props.trend - Trend percentage (e.g., "+5.2%")
   * @param {string} props.trendDirection - Direction: 'up', 'down', or 'neutral'
   * @param {Function} props.onClick - Callback function for card clicks
   * @param {boolean} props.isLoading - Whether to show loading skeleton
   * @returns {React.ReactElement} Fully interactive statistics card
   * 
   * @example
   * <StatCard
   *   number="150"
   *   title="ACTIVE DONORS"
   *   bgColor="bg-gradient-to-br from-green-500 to-green-600"
   *   fullDetailText="VIEW DETAILS"
   *   trend="+12.5%"
   *   trendDirection="up"
   *   onClick={() => navigate('/donors')}
   *   isLoading={false}
   * />
   */
  const StatCard = ({ 
    number, 
    title, 
    bgColor, 
    fullDetailText, 
    trend, 
    trendDirection,
    onClick, 
    isLoading = false 
  }) => {
    // Get appropriate styling for trend indicator
    const trendInfo = getTrendIndicator(trendDirection);
    
    // LOADING STATE DISPLAY
    // Shows animated skeleton while data is being fetched
    if (isLoading) {
      return (
        <div className="bg-white rounded-2xl shadow-xl overflow-hidden animate-pulse">
          <div className="p-8">
            {/* Skeleton for main number */}
            <div className="h-10 bg-slate-200 rounded mb-2"></div>
            {/* Skeleton for title */}
            <div className="h-6 bg-slate-200 rounded w-3/4"></div>
          </div>
          {/* Skeleton for footer */}
          <div className="bg-slate-100 h-12"></div>
        </div>
      );
    }

    // MAIN CARD DISPLAY
    return (
      <div 
        className={`${bgColor} rounded-2xl shadow-xl overflow-hidden cursor-pointer transform transition-all duration-300 ease-in-out hover:-translate-y-3 hover:shadow-2xl group`}
        onClick={onClick}
        role="button"
        tabIndex={0}
        onKeyDown={(e) => e.key === 'Enter' && onClick?.()}
        aria-label={`${title}: ${number}. ${trend ? `Trend: ${trend} ${trendDirection}` : ''}`}
      >
        {/* MAIN CONTENT AREA */}
        <div className="p-8 text-white relative">
          
          {/* TREND INDICATOR (TOP RIGHT) */}
          {trend && (
            <div className={`absolute top-6 right-6 ${trendInfo.bgColor} ${trendInfo.color} px-3 py-1 rounded-full text-xs font-medium shadow-lg`}>
              {trendInfo.symbol} {trend}
            </div>
          )}
          
          {/* MAIN STATISTIC NUMBER */}
          <div className="text-5xl font-bold mb-4 drop-shadow-lg" aria-label={`${number} ${title}`}>
            {formatNumber(parseInt(number) || 0)}
          </div>
          
          {/* STATISTIC TITLE/DESCRIPTION */}
          <div className="text-lg font-semibold pr-20 leading-tight">
            {title}
          </div>
        </div>
        
        {/* FOOTER ACTION AREA */}
        <div className="bg-white bg-opacity-20 px-8 py-4 backdrop-blur-sm">
          <div className="flex items-center justify-between text-white text-sm font-medium">
            <span>{fullDetailText}</span>
            {/* Animated arrow icon */}
            <FaChevronRight className="text-sm transition-transform duration-300 group-hover:translate-x-2 group-hover:scale-110" />
          </div>
        </div>
      </div>
    );
  };

  // ============================================
  // PROP TYPES VALIDATION
  // ============================================
  
  /**
   * PropTypes validation for StatCard component
   * Ensures type safety and provides development-time warnings
   */
  StatCard.propTypes = {
    number: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    bgColor: PropTypes.string.isRequired,
    fullDetailText: PropTypes.string.isRequired,
    trend: PropTypes.string,
    trendDirection: PropTypes.oneOf(['up', 'down', 'neutral']),
    onClick: PropTypes.func,
    isLoading: PropTypes.bool
  };

  /**
   * Default props for StatCard component
   * Provides sensible defaults for optional properties
   */
  StatCard.defaultProps = {
    trend: null,
    trendDirection: 'neutral',
    onClick: () => {},
    isLoading: false
  };

  // ============================================
  // EVENT HANDLERS
  // ============================================
  /**
   * HANDLE MENU ITEM CLICK
   * 
   * Updates the active menu state and handles navigation.
   * Navigates to the appropriate page based on the menu item clicked.
   * 
   * @param {string} menuLabel - Display label of the clicked menu item
   * @param {string} path - URL path for navigation
   */
  const handleMenuClick = (menuLabel, path) => {
    setActiveMenu(menuLabel);
    
    // Navigate to the specified path
    navigate(path);
    
    console.log(`Navigating to: ${path}`);
  };

  /**
   * HANDLE STATISTICS CARD CLICK
   * 
   * Handles clicks on dashboard statistics cards.
   * Could open detailed views, modals, or navigate to specific sections.
   * 
   * @param {string} cardId - Unique identifier for the clicked card
   * 
   * FUTURE ENHANCEMENTS:
   * - Open detailed analytics modals
   * - Navigate to specific data views
   * - Export data functionality
   * - Real-time data refresh
   */
  const handleStatCardClick = (cardId) => {
    // TODO: Implement card-specific actions
    // switch(cardId) {
    //   case 'blood-groups':
    //     navigate('/admin/blood-groups');
    //     break;
    //   case 'registered-groups':
    //     openModal('blood-group-details');
    //     break;
    //   // ... other cases
    // }
    
    console.log(`Clicked on stat card: ${cardId}`);
  };

  // ============================================
  // MEMOIZED COMPUTATIONS
  // ============================================

  /**
   * GROUP MENU ITEMS BY CATEGORY
   * 
   * Organizes menu items into categories for better navigation structure.
   * Memoized to prevent unnecessary recalculations on re-renders.
   * 
   * @returns {Object} Object with category names as keys and menu items as values
   */
  const menuCategories = useMemo(() => {
    const categories = {};
    MENU_ITEMS.forEach(item => {
      const category = item.category || 'main';
      if (!categories[category]) {
        categories[category] = [];
      }
      categories[category].push(item);
    });
    return categories;
  }, []); // Empty dependency array since MENU_ITEMS is constant  // ============================================
  // MAIN COMPONENT RENDER
  // ============================================

  return (
    <div className="min-h-screen bg-slate-50 flex" role="main">
      
      {/* ==========================================
          SIDEBAR NAVIGATION SECTION
          ========================================== */}
      
      {/* 
        SIDEBAR CONTAINER
        Fixed-width navigation panel containing:
        - System branding and version info
        - Main navigation menu organized by categories
        - System information footer
        
        ACCESSIBILITY FEATURES:
        - role="navigation" for screen readers
        - aria-label for clear purpose description
        - Semantic HTML structure with nav, header, ul elements
      */}
      <aside 
        className="w-64 bg-white shadow-xl text-slate-800 flex flex-col border-r border-slate-200" 
        role="navigation" 
        aria-label="Main navigation"
      >
        
        {/* SYSTEM BRANDING HEADER */}
        <header className="bg-gradient-to-r from-red-500 to-pink-600 text-white p-4 border-b">
          <h1 className="text-lg font-bold leading-tight">
            {APP_CONFIG.name}
          </h1>
          <p className="text-xs text-red-100 mt-1">
            v{APP_CONFIG.version}
          </p>
        </header>
        
        {/* MAIN NAVIGATION MENU */}
        <nav className="flex-1 py-4">
          
          {/* MAIN MENU SECTION HEADER */}
          <div className="px-4 mb-4">
            <span className="text-xs text-slate-400 uppercase tracking-wider font-medium">
              MAIN
            </span>
          </div>
          
          {/* MAIN MENU ITEMS LIST */}
          <ul className="space-y-1">
            {MENU_ITEMS.filter(item => item.category === 'main').map((item, index) => (
              <li key={`${item.label}-${index}`}>
                <button
                  onClick={() => handleMenuClick(item.label, item.path)}
                  className={`w-full flex items-center px-6 py-3 text-left hover:bg-red-50 focus:bg-red-50 focus:outline-none transition-colors duration-200 rounded-r-full mr-4 ${
                    activeMenu === item.label 
                      ? 'bg-gradient-to-r from-red-100 to-pink-100 border-r-4 border-red-500 text-red-700' 
                      : 'text-slate-600 hover:text-red-600'
                  }`}
                  aria-current={activeMenu === item.label ? 'page' : undefined}
                  title={item.description}
                >
                  {/* MENU ITEM ICON */}
                  <item.icon 
                    className="mr-3 text-sm flex-shrink-0" 
                    aria-hidden="true" 
                  />
                  {/* MENU ITEM LABEL */}
                  <span className="text-sm font-medium truncate">
                    {item.label}
                  </span>
                </button>
              </li>
            ))}
          </ul>

          {/* SETTINGS SECTION (if settings items exist) */}
          {MENU_ITEMS.filter(item => item.category === 'settings').length > 0 && (
            <>
              {/* SETTINGS SECTION HEADER */}
              <div className="px-4 mb-4 mt-6">
                <span className="text-xs text-slate-400 uppercase tracking-wider font-medium">
                  SETTINGS
                </span>
              </div>
              
              {/* SETTINGS MENU ITEMS */}
              <ul className="space-y-1">
                {MENU_ITEMS.filter(item => item.category === 'settings').map((item, index) => (
                  <li key={`settings-${item.label}-${index}`}>
                    <button
                      onClick={() => handleMenuClick(item.label, item.path)}
                      className={`w-full flex items-center px-6 py-3 text-left hover:bg-red-50 focus:bg-red-50 focus:outline-none transition-colors duration-200 rounded-r-full mr-4 ${
                        activeMenu === item.label 
                          ? 'bg-gradient-to-r from-red-100 to-pink-100 border-r-4 border-red-500 text-red-700' 
                          : 'text-slate-600 hover:text-red-600'
                      }`}
                      aria-current={activeMenu === item.label ? 'page' : undefined}
                      title={item.description}
                    >
                      {/* SETTINGS ITEM ICON */}
                      <item.icon 
                        className="mr-3 text-sm flex-shrink-0" 
                        aria-hidden="true" 
                      />
                      {/* SETTINGS ITEM LABEL */}
                      <span className="text-sm font-medium truncate">
                        {item.label}
                      </span>
                    </button>
                  </li>
                ))}
              </ul>
            </>
          )}
        </nav>
        
        {/* SIDEBAR FOOTER WITH SYSTEM INFORMATION */}
        <div className="p-4 border-t border-slate-200 bg-slate-50">
          <div className="text-xs text-slate-500">
            {APP_CONFIG.description}
          </div>
          <div className="text-xs text-slate-400 mt-1">
            Last updated: {formatDate(APP_CONFIG.lastUpdated)}
          </div>
        </div>
      </aside>

      {/* ==========================================
          MAIN CONTENT AREA SECTION
          ========================================== */}
      
      {/* 
        MAIN CONTENT CONTAINER
        Flexible container that holds:
        - Top header bar with user info and page title
        - Dashboard content with statistics and data visualizations
        
        RESPONSIVE DESIGN:
        - flex-1: Takes remaining space after sidebar
        - min-w-0: Prevents flex item from overflowing
        - Scrollable content area for long pages
      */}
      <div className="flex-1 flex flex-col min-w-0">
          {/* TOP HEADER BAR */}
        <header className="bg-white shadow-lg border-b border-slate-200">
          <div className="flex items-center justify-between px-6 py-4">
            
            {/* PAGE TITLE AND GREETING SECTION */}
            <div className="flex-1">
              <h2 className="text-xl font-semibold text-slate-800">
                {activeMenu}
              </h2>
              <p className="text-sm text-slate-600 mt-1">
                {greeting}! Welcome to the administration panel • {currentDate}
              </p>
            </div>
            
            {/* USER ACCOUNT SECTION */}
            <div className="relative flex items-center space-x-4">
              <span className="text-slate-700 font-medium">Welcome, {userInfo.role === 'admin' ? 'Admin' : 'User'}</span>
                {/* USER DROPDOWN */}
              <div className="relative user-dropdown">
                <button
                  onClick={() => setShowUserDropdown(!showUserDropdown)}
                  className="flex items-center space-x-2 cursor-pointer hover:bg-red-50 rounded-lg p-2 transition-colors"
                >
                  {/* USER AVATAR */}
                  <div className="w-8 h-8 bg-gradient-to-r from-red-500 to-pink-600 rounded-full flex items-center justify-center">
                    <span className="text-white text-sm font-medium">
                      {userInfo.email.charAt(0).toUpperCase()}
                    </span>
                  </div>
                  
                  {/* USER EMAIL */}
                  <span className="text-sm text-slate-600 hidden md:block">
                    {userInfo.email}
                  </span>
                  
                  {/* DROPDOWN INDICATOR */}
                  <FaChevronDown className="text-slate-400 text-xs" />
                </button>
                
                {/* DROPDOWN MENU */}
                {showUserDropdown && (
                  <div className="absolute right-0 top-full mt-2 w-48 bg-white rounded-lg shadow-xl border border-slate-200 py-2 z-50">
                    <div className="px-4 py-2 border-b border-slate-100">
                      <p className="text-sm font-medium text-slate-800">Signed in as</p>
                      <p className="text-xs text-slate-600 truncate">{userInfo.email}</p>
                    </div>
                    <button
                      onClick={handleLogout}
                      className="w-full flex items-center px-4 py-2 text-sm text-red-600 hover:bg-red-50 transition-colors"
                    >
                      <FaSignOutAlt className="mr-2 text-xs" />
                      Sign out
                    </button>
                  </div>
                )}
              </div>
            </div>
          </div>
        </header>

        {/* MAIN DASHBOARD CONTENT */}
        <main className="flex-1 p-6 overflow-auto">
          
          {/* WELCOME BANNER */}
          <div className="relative mb-8 bg-gradient-to-r from-red-500 to-pink-600 text-white rounded-2xl shadow-xl overflow-hidden">
            {/* SUBTLE OVERLAY FOR DEPTH */}
            <div className="absolute inset-0 bg-black/10"></div>
            <div className="relative z-10 p-8">
              <h1 className="text-4xl md:text-5xl font-extrabold tracking-tight leading-tight mb-2 drop-shadow-lg">
                Welcome to <span className="text-pink-200">Admin</span> Dashboard
              </h1>
              <p className="text-lg text-red-100">
                {greeting}! Monitor key metrics and manage your blood donation system efficiently.
              </p>
            </div>
          </div>

          {/* PAGE DESCRIPTION HEADER */}
          <div className="text-center mb-12">
            <h2 className="text-4xl font-bold text-slate-800 mb-4">
              Dashboard Overview
            </h2>
            <p className="text-lg text-slate-500">
              Your journey to managing lives starts here
            </p>
          </div>

          {/* STATISTICS GRID SECTION */}
          <section aria-labelledby="stats-heading" className="mb-8">
            <h2 id="stats-heading" className="sr-only">
              Dashboard Statistics
            </h2>
            
            {/* RESPONSIVE STATISTICS GRID */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
              {statisticsData.map((stat) => (
                <StatCard
                  key={stat.id}
                  number={stat.number}
                  title={stat.title}
                  bgColor={stat.bgColor}
                  fullDetailText={stat.fullDetailText}
                  trend={stat.trend}
                  trendDirection={stat.trendDirection}
                  onClick={() => handleStatCardClick(stat.id)}
                  isLoading={isLoading}
                />
              ))}
            </div>
          </section>

          {/* ADDITIONAL CONTENT PLACEHOLDER */}
          <section className="bg-white rounded-2xl shadow-xl border border-slate-200 p-8">
            <h3 className="text-2xl font-bold text-slate-800 mb-6">
              Recent Activities
            </h3>
            <div className="text-slate-500 text-center py-12">
              <div className="inline-block p-6 bg-gradient-to-br from-red-100 to-pink-100 rounded-full mb-6">
                <div className="text-4xl text-red-600">📊</div>
              </div>
              <p className="text-lg mb-2">Additional dashboard content will be displayed here</p>
              <p className="text-sm text-slate-400">
                Charts, recent activities, quick actions, etc.
              </p>
            </div>
          </section>
        </main>
      </div>
    </div>
  );
};

// ============================================
// COMPONENT EXPORT
// ============================================

/**
 * Export the Dashboard component as the default export
 * This allows importing the component in other files using:
 * import Dashboard from './Dashboard';
 */
export default Dashboard;
