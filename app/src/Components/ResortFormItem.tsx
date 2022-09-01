import React, { PropsWithChildren } from "react";

export type FormItemVariant = "dropdown" | "input";

interface Props {
  label: string;
  variant: FormItemVariant;
}

const VARIANT_MAPS: Record<FormItemVariant, string> = {
  input: "input",
  dropdown: "dropdown",
};

const ResortFormItem: React.FC<PropsWithChildren<Props>> = (props) => {
  const { label, variant } = props;
  return (
    <div className="mx-auto items-center mb-6">
      {VARIANT_MAPS[variant] === "dropdown" && (
        <div>
          <label className="block text-gray-500 text-left mb-1">{label}</label>
          <select className="border-2 border-gray-300 h-10 w-full px-5 rounded-lg text-sm focus:outline-none">
            <option selected></option>
            <option value="Placeholder">Placeholder</option>
          </select>
        </div>
      )}
      {VARIANT_MAPS[variant] === "input" && (
        <div>
          <label className="block text-gray-500 text-left mb-1">{label}</label>
          <input
            className="border-2 border-gray-300 h-10 w-full px-5 rounded-lg text-sm focus:outline-none"
            id="inline-full-name"
            type="text"
          />
        </div>
      )}
    </div>
  );
};

export default ResortFormItem;
