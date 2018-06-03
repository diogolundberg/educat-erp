<template>
  <span :is="container">
    <label
      v-for="error in errors"
      :key="error"
      class="block red h7">
      {{ error }}
    </label>
    <label
      v-if="label"
      class="block my2 dim">
      {{ label }}
    </label>
    <span
      v-for="option in options"
      :is="container"
      :key="option[idField]"
      class="pointer my2"
      role="radiogroup"
      @click="!disabled && $emit('input', option[idField])">
      <Radio
        :value="option[idField] == value" />
      <label class="mx1 pointer">
        {{ option[labelField] }}
      </label>
    </span>
  </span>
</template>

<script>
  export default {
    name: "RadioGroup",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      errors: {
        type: Array,
        required: false,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
      label: {
        type: String,
        required: false,
        default: null,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
