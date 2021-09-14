import { useForm } from "../../../hooks/useForm";

function Shopper({ onSubmit }) {
  const handleSubmit = (evt) => {
    evt.preventDefault();
    onSubmit(shopper);
  };

  const [shopper, handleChange] = useForm({
    firstName: "John",
    lastName: "Doe",
    addressLine1: "Main street 7",
    addressLine2: "",
    postalCode: "A13 BC15",
    city: "Edmonton",
    state: "Alberta",
    country: "Canada",
    phoneNumber: "12345678",
    emailAddress: "john.doe@doe.com",
  });

  return (
    <div>
      <form className="w-full max-w-lg" onSubmit={handleSubmit}>
        <div className="flex flex-wrap mb-3">
          <pre className="font-sans text-xl px-3">
            Enter your name and address:
          </pre>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-first-name"
              type="text"
              placeholder="First Name"
              name="firstName"
              value={shopper.firstName}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-last-name"
              type="text"
              placeholder="Last Name"
              name="lastName"
              value={shopper.lastName}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-address-line-1"
              type="text"
              placeholder="Address Line 1"
              name="addressLine1"
              value={shopper.addressLine1}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-address-line-2"
              type="text"
              placeholder="Address Line 2"
              name="addressLine2"
              value={shopper.addressLine2}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-2">
          <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <input
              className="appearance-none block w-full bg-white text-black border border-gray-300 rounded py-3 px-4 leading-tight focus:outline-none"
              id="grid-postal-code"
              type="text"
              placeholder="Postal Code"
              name="postalCode"
              value={shopper.postalCode}
              onChange={handleChange}
              required
            />
          </div>
          <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <input
              className="appearance-none block w-full bg-white text-black border border-gray-300 rounded py-3 px-4 leading-tight focus:outline-none"
              id="grid-city"
              type="text"
              placeholder="City"
              name="city"
              value={shopper.city}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-6">
          <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <div className="relative">
              <select
                className="block appearance-none w-full bg-white border border-gray-300 text-black py-3 px-4 pr-8 rounded leading-tight focus:outline-none"
                id="grid-state"
                name="state"
                value={shopper.state}
                onChange={handleChange}
                required
              >
                <option>Select a state</option>
                <option>Alberta</option>
                <option>British Columbia</option>
                <option>Manitoba</option>
                <option>New Brunswick</option>
                <option>Newfoundland and Labrador</option>
                <option>Northwest Territories</option>
                <option>Nova Scotia</option>
                <option>Nunavut</option>
                <option>Ontario</option>
                <option>Prince Edward Island</option>
                <option>Quebec</option>
                <option>Saskatchewan</option>
                <option>Yukon</option>
              </select>
              <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                <svg
                  className="fill-current h-4 w-4"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                </svg>
              </div>
            </div>
          </div>
          <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <input
              className="appearance-none block w-full bg-white text-black border border-gray-300 rounded py-3 px-4 leading-tight focus:outline-none"
              id="grid-country"
              type="text"
              placeholder="Country"
              name="country"
              value={shopper.country}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3">
          <pre className="font-sans text-xl px-3">
            What's your contact information?
          </pre>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-email-address"
              type="text"
              placeholder="Email"
              name="emailAddress"
              value={shopper.emailAddress}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3">
          <div className="w-full px-3">
            <input
              className="appearance-none block w-full bg-white text-black border rounded border-gray-300 py-3 px-4 mb-3 leading-tight focus:outline-none"
              id="grid-phone-number"
              type="text"
              placeholder="Phone Number"
              name="phoneNumber"
              value={shopper.phoneNumber}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className="flex flex-wrap mb-3 justify-center">
          <input
            type="submit"
            className="rounded-full py-3 px-6 bg-[#171717] hover:bg-gray-700 w-full text-white cursor-pointer"
            value="Continue"
          />
        </div>
      </form>
    </div>
  );
}

export default Shopper;
