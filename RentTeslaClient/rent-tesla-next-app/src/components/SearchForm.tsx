"use client";

import { FC, useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, useWatch } from "react-hook-form";
import * as z from "zod";

import { Button } from "@/components/ui/button";

import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { useToast } from "@/components/ui/use-toast";
import DatePickerWithRange from "./ui/data-range-picker";
import { Input } from "./ui/input";
import { Checkbox } from "./ui/checkbox";
import { DateRange } from "react-day-picker";
import { addDays } from "date-fns";

const FormSchema = z.object({
  pickupLocation: z.string({
    required_error: "Pick-up location is required",
  }),
  diffrentLocalation: z.boolean(),
  dropoffLocation: z.string(),
  rangeDate: z.object({
    from: z.date({
      required_error: "Pick-up date is required.",
    }),
    to: z.date({
      required_error: "Drop-off date is required.",
    }),
  }),
});

type FormSchemaValues = z.infer<typeof FormSchema>;

const defaultValues: Partial<FormSchemaValues> = {
  rangeDate: {
    from: new Date(Date.now()),
    to: addDays(Date.now(), 20),
  },
};

interface SearchFormProps {}

const SearchForm: FC<SearchFormProps> = () => {
  const { toast } = useToast();
  const form = useForm<z.infer<typeof FormSchema>>({
    resolver: zodResolver(FormSchema),
    defaultValues,
  });

  function selectDate(data: DateRange | undefined) {
    if (data?.from !== null && data !== undefined) {
      form.setValue("rangeDate.from", data.from!);
    }
    console.log(data?.to);
    if (data?.to !== null && data !== undefined) {
      form.setValue("rangeDate.to", data.to!);
    }
  }

  function onSubmit(data: z.infer<typeof FormSchema>) {
    console.log(data);
    toast({
      title: "You submitted the following values:",
      description: (
        <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
          <code className="text-white">{JSON.stringify(data, null, 2)}</code>
        </pre>
      ),
    });
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="pickupLocation"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Pick-up Location</FormLabel>
              <FormControl>
                <Input placeholder="Pick-up Location" {...field} />
              </FormControl>
              <FormDescription>
                This is your public display name.
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="diffrentLocalation"
          render={({ field }) => (
            <FormItem className="flex flex-row items-center">
              <FormControl>
                <Checkbox
                  checked={field.value}
                  onCheckedChange={field.onChange}
                />
              </FormControl>
              <FormLabel>Return in diffrent location</FormLabel>
              <FormMessage />
            </FormItem>
          )}
        />
        {form.watch("diffrentLocalation") && (
          <FormField
            control={form.control}
            name="dropoffLocation"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Drop-off Location</FormLabel>
                <FormControl>
                  <Input placeholder="Drop-off Location" {...field} />
                </FormControl>
                <FormDescription>
                  This is your public display name.
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        <FormField
          control={form.control}
          name="rangeDate"
          render={({ field }) => (
            <FormItem className="flex flex-col items-center">
              <FormLabel>Date</FormLabel>
              <FormControl>
                <DatePickerWithRange
                  date={form.watch("rangeDate")}
                  handleSelect={selectDate}
                />
              </FormControl>
              <FormDescription>Choose dates when you need car.</FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">Find Cars</Button>
      </form>
    </Form>
  );
};

export default SearchForm;
