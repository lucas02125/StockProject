import React, { SyntheticEvent } from "react";

interface Props {
  OnSubmitPortfolio: (e: SyntheticEvent) => void;
  symbol: string;
}

const AddPortfolio = ({ OnSubmitPortfolio, symbol }: Props) => {
  return (
    <form onSubmit={OnSubmitPortfolio}>
      <input readOnly={true} hidden={true} value={symbol} />
      <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
        Add
      </button>
    </form>
  );
};

export default AddPortfolio;
