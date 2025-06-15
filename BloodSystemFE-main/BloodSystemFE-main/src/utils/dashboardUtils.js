/**
 * Dashboard Utilities
 * 
 * Helper functions and utilities for the admin dashboard
 * Contains reusable logic for data processing, formatting, and validation
 * 
 * @author Your Name
 * @version 1.0.0
 */

/**
 * Format large numbers with appropriate suffixes (K, M, B)
 * 
 * @param {number} num - Number to format
 * @param {number} digits - Number of decimal places
 * @returns {string} Formatted number string
 * 
 * @example
 * formatNumber(1234) // "1.2K"
 * formatNumber(1234567) // "1.2M"
 */
export const formatNumber = (num, digits = 1) => {
  const si = [
    { value: 1, symbol: "" },
    { value: 1E3, symbol: "K" },
    { value: 1E6, symbol: "M" },
    { value: 1E9, symbol: "B" },
    { value: 1E12, symbol: "T" },
    { value: 1E15, symbol: "P" },
    { value: 1E18, symbol: "E" }
  ];
  
  const rx = /\.0+$|(\.[0-9]*[1-9])0+$/;
  let i;
  
  for (i = si.length - 1; i > 0; i--) {
    if (num >= si[i].value) {
      break;
    }
  }
  
  return (num / si[i].value).toFixed(digits).replace(rx, "$1") + si[i].symbol;
};

/**
 * Calculate percentage change between two values
 * 
 * @param {number} current - Current value
 * @param {number} previous - Previous value
 * @returns {Object} Object with percentage and direction
 * 
 * @example
 * calculatePercentageChange(120, 100) // { percentage: 20, direction: 'up' }
 */
export const calculatePercentageChange = (current, previous) => {
  if (previous === 0) {
    return { percentage: current > 0 ? 100 : 0, direction: current > 0 ? 'up' : 'neutral' };
  }
  
  const change = ((current - previous) / previous) * 100;
  const direction = change > 0 ? 'up' : change < 0 ? 'down' : 'neutral';
  
  return {
    percentage: Math.abs(change).toFixed(1),
    direction,
    raw: change
  };
};

/**
 * Get trend indicator based on percentage change
 * 
 * @param {number} percentage - Percentage change
 * @returns {Object} Trend information with color and symbol
 */
export const getTrendIndicator = (percentage) => {
  if (percentage > 0) {
    return {
      color: 'text-green-600',
      bgColor: 'bg-green-100',
      symbol: '↗',
      label: 'increase'
    };
  } else if (percentage < 0) {
    return {
      color: 'text-red-600',
      bgColor: 'bg-red-100',
      symbol: '↘',
      label: 'decrease'
    };
  } else {
    return {
      color: 'text-gray-600',
      bgColor: 'bg-gray-100',
      symbol: '→',
      label: 'no change'
    };
  }
};

/**
 * Debounce function to limit the rate of function execution
 * 
 * @param {Function} func - Function to debounce
 * @param {number} wait - Wait time in milliseconds
 * @param {boolean} immediate - Whether to execute immediately
 * @returns {Function} Debounced function
 */
export const debounce = (func, wait, immediate = false) => {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      timeout = null;
      if (!immediate) func(...args);
    };
    const callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
    if (callNow) func(...args);
  };
};

/**
 * Validate menu item structure
 * 
 * @param {Object} menuItem - Menu item to validate
 * @returns {boolean} Whether the menu item is valid
 */
export const validateMenuItem = (menuItem) => {
  const requiredFields = ['icon', 'label', 'path'];
  return requiredFields.every(field => menuItem.hasOwnProperty(field) && menuItem[field]);
};

/**
 * Filter menu items by category
 * 
 * @param {Array} menuItems - Array of menu items
 * @param {string} category - Category to filter by
 * @returns {Array} Filtered menu items
 */
export const filterMenuItemsByCategory = (menuItems, category) => {
  return menuItems.filter(item => item.category === category);
};

/**
 * Group menu items by category
 * 
 * @param {Array} menuItems - Array of menu items
 * @returns {Object} Object with categories as keys and items as values
 */
export const groupMenuItemsByCategory = (menuItems) => {
  return menuItems.reduce((groups, item) => {
    const category = item.category || 'uncategorized';
    if (!groups[category]) {
      groups[category] = [];
    }
    groups[category].push(item);
    return groups;
  }, {});
};

/**
 * Generate unique ID for components
 * 
 * @param {string} prefix - Prefix for the ID
 * @returns {string} Unique ID
 */
export const generateId = (prefix = 'id') => {
  return `${prefix}-${Date.now()}-${Math.random().toString(36).substr(2, 9)}`;
};

/**
 * Check if user has permission for a specific action
 * 
 * @param {Array} userPermissions - User's permissions array
 * @param {string} requiredPermission - Required permission
 * @returns {boolean} Whether user has permission
 */
export const hasPermission = (userPermissions, requiredPermission) => {
  if (!Array.isArray(userPermissions)) return false;
  return userPermissions.includes(requiredPermission) || userPermissions.includes('admin');
};

/**
 * Format date for display
 * 
 * @param {Date|string} date - Date to format
 * @param {Object} options - Intl.DateTimeFormat options
 * @returns {string} Formatted date string
 */
export const formatDate = (date, options = {}) => {
  const defaultOptions = {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    ...options
  };
  
  return new Intl.DateTimeFormat('en-US', defaultOptions).format(new Date(date));
};

/**
 * Get greeting based on current time
 * 
 * @returns {string} Appropriate greeting
 */
export const getTimeBasedGreeting = () => {
  const hour = new Date().getHours();
  
  if (hour < 12) return 'Good morning';
  if (hour < 17) return 'Good afternoon';
  return 'Good evening';
};

/**
 * Local storage utilities with error handling
 */
export const storage = {
  /**
   * Get item from localStorage
   * 
   * @param {string} key - Storage key
   * @param {*} defaultValue - Default value if key doesn't exist
   * @returns {*} Stored value or default value
   */
  get: (key, defaultValue = null) => {
    try {
      const item = localStorage.getItem(key);
      return item ? JSON.parse(item) : defaultValue;
    } catch (error) {
      console.error(`Error reading from localStorage:`, error);
      return defaultValue;
    }
  },

  /**
   * Set item in localStorage
   * 
   * @param {string} key - Storage key
   * @param {*} value - Value to store
   * @returns {boolean} Whether operation was successful
   */
  set: (key, value) => {
    try {
      localStorage.setItem(key, JSON.stringify(value));
      return true;
    } catch (error) {
      console.error(`Error writing to localStorage:`, error);
      return false;
    }
  },

  /**
   * Remove item from localStorage
   * 
   * @param {string} key - Storage key
   * @returns {boolean} Whether operation was successful
   */
  remove: (key) => {
    try {
      localStorage.removeItem(key);
      return true;
    } catch (error) {
      console.error(`Error removing from localStorage:`, error);
      return false;
    }
  }
};
