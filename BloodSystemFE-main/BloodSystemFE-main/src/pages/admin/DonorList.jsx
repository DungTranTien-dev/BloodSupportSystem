import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  FaArrowLeft, 
  FaPlus, 
  FaEye, 
  FaDownload, 
  FaCertificate, 
  FaIdCard, 
  FaHeart,
  FaSearch,
  FaFilter,
  FaEdit,
  FaTrash
} from 'react-icons/fa';

// Sample donor data
const sampleDonors = [
  {
    id: 1,
    donorNo: '222',
    donorName: 'Anupama',
    age: 49,
    gender: 'Female',
    mobile: '9959525222',
    city: 'Vellore',
    district: 'Dharmapuri',
    state: 'Tamilnadu',
    donorType: '-',
    bloodGroup: 'A+',
    rhType: 'Positive',
    donationHistory: [
      { date: 'Mar 28, 2023', volume: '450.00ml', anyCritical: '-' },
      { date: 'May 4, 2023', volume: '350.00ml', anyCritical: '-' }
    ],
    deferralHistory: [
      { date: 'Oct 26, 2021', reason: 'Fever', type: 'Temporary', notes: '-' }
    ]
  },
  {
    id: 2,
    donorNo: '223',
    donorName: 'Rajesh Kumar',
    age: 35,
    gender: 'Male',
    mobile: '9876543210',
    city: 'Chennai',
    district: 'Chennai',
    state: 'Tamilnadu',
    donorType: 'Regular',
    bloodGroup: 'B+',
    rhType: 'Positive',
    donationHistory: [
      { date: 'Jan 15, 2023', volume: '450.00ml', anyCritical: '-' },
      { date: 'Apr 20, 2023', volume: '450.00ml', anyCritical: '-' }
    ],
    deferralHistory: []
  },
  {
    id: 3,
    donorNo: '224',
    donorName: 'Priya Sharma',
    age: 28,
    gender: 'Female',
    mobile: '9123456789',
    city: 'Bangalore',
    district: 'Bangalore Urban',
    state: 'Karnataka',
    donorType: 'First Time',
    bloodGroup: 'O-',
    rhType: 'Negative',
    donationHistory: [
      { date: 'Feb 10, 2023', volume: '450.00ml', anyCritical: '-' }
    ],
    deferralHistory: []
  }
];

const DonorList = () => {
  const navigate = useNavigate();
  const [donors, setDonors] = useState([]);
  const [selectedDonor, setSelectedDonor] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterBy, setFilterBy] = useState('all');  const [isLoading, setIsLoading] = useState(false);

  // Load donors from localStorage on component mount
  useEffect(() => {
    const loadDonors = () => {
      const savedDonors = JSON.parse(localStorage.getItem('donors') || '[]');
      // Mark sample donors vs new donors
      const sampleDonorsWithFlag = sampleDonors.map(donor => ({ ...donor, isNew: false }));
      const newDonorsWithFlag = savedDonors.map(donor => ({ ...donor, isNew: true }));
      const allDonors = [...sampleDonorsWithFlag, ...newDonorsWithFlag];
      setDonors(allDonors);
    };
    
    loadDonors();

    // Listen for storage changes (when new donor is added)
    const handleStorageChange = () => {
      loadDonors();
    };

    window.addEventListener('storage', handleStorageChange);
    // Also listen for focus events (when returning from add donor page)
    window.addEventListener('focus', loadDonors);

    return () => {
      window.removeEventListener('storage', handleStorageChange);
      window.removeEventListener('focus', loadDonors);
    };
  }, []);

  // Filter donors based on search and filter criteria
  const filteredDonors = donors.filter(donor => {
    const matchesSearch = donor.donorName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         donor.donorNo.includes(searchTerm) ||
                         donor.mobile.includes(searchTerm);
    
    const matchesFilter = filterBy === 'all' || 
                         donor.bloodGroup === filterBy ||
                         donor.gender.toLowerCase() === filterBy.toLowerCase();
    
    return matchesSearch && matchesFilter;
  });

  const handleViewDonor = (donor) => {
    setSelectedDonor(donor);
  };

  const handleBackToList = () => {
    setSelectedDonor(null);
  };

  const handleBackToDashboard = () => {
    navigate('/admin/dashboard');
  };

  const handleCollectBlood = (donor) => {
    console.log('Collect blood for donor:', donor.donorName);
    // TODO: Implement blood collection logic
  };

  const handleBillPayment = (donor) => {
    console.log('Bill payment for donor:', donor.donorName);
    // TODO: Implement bill payment logic
  };

  const handleCertificate = (donor) => {
    console.log('Generate certificate for donor:', donor.donorName);
    // TODO: Implement certificate generation
  };

  const handleDonorCard = (donor) => {
    console.log('Generate donor card for donor:', donor.donorName);
    // TODO: Implement donor card generation
  };
  const handleThanksLetter = (donor) => {
    console.log('Generate thanks letter for donor:', donor.donorName);
    // TODO: Implement thanks letter generation
  };

  const handleEditDonor = (donor) => {
    console.log('Edit donor:', donor.id);
    // TODO: Navigate to edit form with donor data
    // navigate(`/admin/edit-donor/${donor.id}`);
  };
  const handleDeleteDonor = (donorId, donorName) => {
    if (window.confirm(`Are you sure you want to delete ${donorName}? This action cannot be undone.`)) {
      // Remove from localStorage (only if it's a custom added donor)
      const savedDonors = JSON.parse(localStorage.getItem('donors') || '[]');
      const updatedSavedDonors = savedDonors.filter(donor => donor.id !== donorId);
      localStorage.setItem('donors', JSON.stringify(updatedSavedDonors));
      
      // Update local state
      setDonors(prevDonors => prevDonors.filter(donor => donor.id !== donorId));
      
      alert(`${donorName} has been deleted successfully!`);
    }
  };

  // If viewing a specific donor
  if (selectedDonor) {
    return (
      <div className="min-h-screen bg-slate-50">
        {/* Header */}
        <div className="bg-white shadow-sm border-b border-slate-200 px-6 py-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center space-x-4">
              <button
                onClick={handleBackToList}
                className="flex items-center space-x-2 text-slate-600 hover:text-red-600 transition-colors"
              >
                <FaArrowLeft className="text-sm" />
                <span className="font-medium">Back</span>
              </button>
              <h1 className="text-2xl font-bold text-slate-800">Donor Details</h1>
            </div>
            <button
              onClick={() => navigate('/admin/add-donor')}
              className="flex items-center space-x-2 bg-gradient-to-r from-red-500 to-pink-600 text-white px-4 py-2 rounded-lg hover:opacity-90 transition-opacity"
            >
              <FaPlus className="text-sm" />
              <span>Add Donor</span>
            </button>
          </div>
        </div>

        {/* Donor Details Content */}
        <div className="p-6">
          <div className="bg-white rounded-xl shadow-lg overflow-hidden">
            
            {/* Donor Info Header */}
            <div className="bg-gradient-to-r from-red-500 to-pink-600 text-white p-6">
              <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div>
                  <h3 className="text-lg font-semibold mb-2">Donor Details</h3>
                  <div className="space-y-1 text-sm">
                    <p><span className="font-medium">Donor Name:</span> {selectedDonor.donorName}</p>
                    <p><span className="font-medium">Donor No:</span> {selectedDonor.donorNo}</p>
                    <p><span className="font-medium">Age/Gender:</span> {selectedDonor.age}/ {selectedDonor.gender}</p>
                    <p><span className="font-medium">Mobile:</span> {selectedDonor.mobile}</p>
                  </div>
                </div>
                <div>
                  <h3 className="text-lg font-semibold mb-2">Location</h3>
                  <div className="space-y-1 text-sm">
                    <p><span className="font-medium">City:</span> {selectedDonor.city}</p>
                    <p><span className="font-medium">District:</span> {selectedDonor.district}</p>
                    <p><span className="font-medium">State:</span> {selectedDonor.state}</p>
                  </div>
                </div>
                <div>
                  <h3 className="text-lg font-semibold mb-2">Blood Info</h3>
                  <div className="space-y-1 text-sm">
                    <p><span className="font-medium">Donor type:</span> {selectedDonor.donorType}</p>
                    <p><span className="font-medium">Blood group:</span> {selectedDonor.bloodGroup}</p>
                    <p><span className="font-medium">Rh type:</span> {selectedDonor.rhType}</p>
                  </div>
                </div>
              </div>
            </div>

            {/* Action Buttons */}
            <div className="p-6 border-b border-slate-200">
              <div className="flex flex-wrap gap-3">
                <button
                  onClick={() => handleCollectBlood(selectedDonor)}
                  className="flex items-center space-x-2 bg-red-600 text-white px-4 py-2 rounded-lg hover:bg-red-700 transition-colors"
                >
                  <FaHeart className="text-sm" />
                  <span>Collect Blood</span>
                </button>
                <button
                  onClick={() => handleBillPayment(selectedDonor)}
                  className="flex items-center space-x-2 bg-green-600 text-white px-4 py-2 rounded-lg hover:bg-green-700 transition-colors"
                >
                  <FaDownload className="text-sm" />
                  <span>Bill payment</span>
                </button>
                <button
                  onClick={() => handleCertificate(selectedDonor)}
                  className="flex items-center space-x-2 bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors"
                >
                  <FaCertificate className="text-sm" />
                  <span>Certificate</span>
                </button>
                <button
                  onClick={() => handleDonorCard(selectedDonor)}
                  className="flex items-center space-x-2 bg-purple-600 text-white px-4 py-2 rounded-lg hover:bg-purple-700 transition-colors"
                >
                  <FaIdCard className="text-sm" />
                  <span>Donor Card</span>
                </button>
                <button
                  onClick={() => handleThanksLetter(selectedDonor)}
                  className="flex items-center space-x-2 bg-pink-600 text-white px-4 py-2 rounded-lg hover:bg-pink-700 transition-colors"
                >
                  <FaHeart className="text-sm" />
                  <span>Thanks letter</span>
                </button>
              </div>
            </div>

            {/* Donor History Tables */}
            <div className="p-6">
              {/* Donation History */}
              <div className="mb-8">
                <h3 className="text-lg font-semibold text-slate-800 mb-4">Donor Details</h3>
                <div className="overflow-x-auto">
                  <table className="w-full border-collapse border border-slate-300">
                    <thead>
                      <tr className="bg-slate-100">
                        <th className="border border-slate-300 px-4 py-2 text-left">#</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Donate Date</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Volume</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Any Critical</th>
                      </tr>
                    </thead>
                    <tbody>
                      {selectedDonor.donationHistory.map((donation, index) => (
                        <tr key={index} className="hover:bg-slate-50">
                          <td className="border border-slate-300 px-4 py-2">{index + 1}</td>
                          <td className="border border-slate-300 px-4 py-2">{donation.date}</td>
                          <td className="border border-slate-300 px-4 py-2">{donation.volume}</td>
                          <td className="border border-slate-300 px-4 py-2">{donation.anyCritical}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>

              {/* Deferral History */}
              <div>
                <h3 className="text-lg font-semibold text-slate-800 mb-4">Donor Deferral</h3>
                <div className="overflow-x-auto">
                  <table className="w-full border-collapse border border-slate-300">
                    <thead>
                      <tr className="bg-slate-100">
                        <th className="border border-slate-300 px-4 py-2 text-left">#</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Date</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Reason for Deferral</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Type</th>
                        <th className="border border-slate-300 px-4 py-2 text-left">Notes</th>
                      </tr>
                    </thead>
                    <tbody>
                      {selectedDonor.deferralHistory.length > 0 ? (
                        selectedDonor.deferralHistory.map((deferral, index) => (
                          <tr key={index} className="hover:bg-slate-50">
                            <td className="border border-slate-300 px-4 py-2">{index + 1}</td>
                            <td className="border border-slate-300 px-4 py-2">{deferral.date}</td>
                            <td className="border border-slate-300 px-4 py-2">{deferral.reason}</td>
                            <td className="border border-slate-300 px-4 py-2">{deferral.type}</td>
                            <td className="border border-slate-300 px-4 py-2">{deferral.notes}</td>
                          </tr>
                        ))
                      ) : (
                        <tr>
                          <td colSpan="5" className="border border-slate-300 px-4 py-8 text-center text-slate-500">
                            No deferral history available
                          </td>
                        </tr>
                      )}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  // Main donor list view
  return (
    <div className="min-h-screen bg-slate-50">
      {/* Header */}
      <div className="bg-white shadow-sm border-b border-slate-200 px-6 py-4">
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <button
              onClick={handleBackToDashboard}
              className="flex items-center space-x-2 text-slate-600 hover:text-red-600 transition-colors"
            >
              <FaArrowLeft className="text-sm" />
              <span className="font-medium">Back to Dashboard</span>
            </button>
            <h1 className="text-2xl font-bold text-slate-800">Donor List</h1>
          </div>
          <button
            onClick={() => navigate('/admin/add-donor')}
            className="flex items-center space-x-2 bg-gradient-to-r from-red-500 to-pink-600 text-white px-4 py-2 rounded-lg hover:opacity-90 transition-opacity"
          >
            <FaPlus className="text-sm" />
            <span>Add Donor</span>
          </button>
        </div>
      </div>

      {/* Search and Filter */}
      <div className="p-6">
        <div className="bg-white rounded-xl shadow-sm p-6 mb-6">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between space-y-4 md:space-y-0 md:space-x-4">
            {/* Search */}
            <div className="relative flex-1 max-w-md">
              <FaSearch className="absolute left-3 top-1/2 transform -translate-y-1/2 text-slate-400" />
              <input
                type="text"
                placeholder="Search by name, donor no, or mobile..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="w-full pl-10 pr-4 py-2 border border-slate-300 rounded-lg focus:ring-2 focus:ring-red-500 focus:border-red-500"
              />
            </div>

            {/* Filter */}
            <div className="flex items-center space-x-2">
              <FaFilter className="text-slate-400" />
              <select
                value={filterBy}
                onChange={(e) => setFilterBy(e.target.value)}
                className="px-4 py-2 border border-slate-300 rounded-lg focus:ring-2 focus:ring-red-500 focus:border-red-500"
              >
                <option value="all">All Donors</option>
                <option value="A+">A+ Blood Group</option>
                <option value="A-">A- Blood Group</option>
                <option value="B+">B+ Blood Group</option>
                <option value="B-">B- Blood Group</option>
                <option value="AB+">AB+ Blood Group</option>
                <option value="AB-">AB- Blood Group</option>
                <option value="O+">O+ Blood Group</option>
                <option value="O-">O- Blood Group</option>
                <option value="male">Male</option>
                <option value="female">Female</option>
              </select>
            </div>
          </div>
        </div>

        {/* Donor Table */}
        <div className="bg-white rounded-xl shadow-sm overflow-hidden">
          <div className="p-6 border-b border-slate-200">
            <h2 className="text-lg font-semibold text-slate-800">
              Total Donors: {filteredDonors.length}
            </h2>
          </div>
          
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-slate-50 border-b border-slate-200">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-slate-500 uppercase tracking-wider">
                    Donor Info
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-slate-500 uppercase tracking-wider">
                    Contact
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-slate-500 uppercase tracking-wider">
                    Blood Group
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-slate-500 uppercase tracking-wider">
                    Location
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-slate-500 uppercase tracking-wider">
                    Actions
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-slate-200">
                {filteredDonors.map((donor) => (
                  <tr key={donor.id} className="hover:bg-slate-50">                    <td className="px-6 py-4 whitespace-nowrap">
                      <div>
                        <div className="flex items-center space-x-2">
                          <span className="text-sm font-medium text-slate-900">{donor.donorName}</span>
                          {donor.isNew && (
                            <span className="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800">
                              New
                            </span>
                          )}
                        </div>
                        <div className="text-sm text-slate-500">#{donor.donorNo}</div>
                        <div className="text-sm text-slate-500">{donor.age} years, {donor.gender}</div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-slate-900">{donor.mobile}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-red-100 text-red-800">
                        {donor.bloodGroup}
                      </span>
                      <div className="text-sm text-slate-500 mt-1">{donor.rhType}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-slate-900">{donor.city}</div>
                      <div className="text-sm text-slate-500">{donor.state}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex space-x-2">
                        <button
                          onClick={() => handleViewDonor(donor)}
                          className="text-blue-600 hover:text-blue-900 flex items-center space-x-1"
                          title="View Details"
                        >
                          <FaEye className="text-sm" />
                          <span>View</span>
                        </button>                        <button
                          onClick={() => handleEditDonor(donor)}
                          className="text-green-600 hover:text-green-900 flex items-center space-x-1"
                          title="Edit Donor"
                        >
                          <FaEdit className="text-sm" />
                          <span>Edit</span>
                        </button>
                        {donor.isNew && (
                          <button
                            onClick={() => handleDeleteDonor(donor.id, donor.donorName)}
                            className="text-red-600 hover:text-red-900 flex items-center space-x-1"
                            title="Delete Donor"
                          >
                            <FaTrash className="text-sm" />
                            <span>Delete</span>
                          </button>
                        )}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>

            {filteredDonors.length === 0 && (
              <div className="text-center py-12">
                <div className="text-slate-500">
                  <FaSearch className="mx-auto text-4xl mb-4" />
                  <p className="text-lg font-medium">No donors found</p>
                  <p className="text-sm">Try adjusting your search or filter criteria</p>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default DonorList;
